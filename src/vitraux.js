"use strict";

const vmUpdateFunctionKeyPrefix = "vitraux-vms-";

class VitrauxInternalError extends Error {
    constructor(message) {
        const internalErrorTitle = "Vitraux Internal Error:";

        super(`${internalErrorTitle} ${message}`);
    }
}

class ActionDispatcherProvider {
    #vmKey;
    #actionKey;
    #actionArgsCallback;
    #vitrauxWasmExports;

    constructor(vmKey, actionKey, actionArgsCallback) {
        this.#vmKey = vmKey;
        this.#actionKey = actionKey;
        this.#actionArgsCallback = actionArgsCallback;
    }

    async getDispatcherInfo(event) {
        const currentActionArgs = this.#actionArgsCallback.call(null, event);
        const vitrauxWasm = await this.#getVitrauxWasmExports();

        return {
            vmKey: this.#vmKey,
            actionKey: this.#actionKey,
            currentActionArgs: currentActionArgs,
            dispatcher: vitrauxWasm
        };
    }

    async #getVitrauxWasmExports() {
        if (!this.#vitrauxWasmExports) {

            const getAssemblyExports = await globalThis.getDotnetRuntime(0);
            this.#vitrauxWasmExports = await getAssemblyExports("Vitraux.dll");
        }

        return this.#vitrauxWasmExports;
    }
}

class ActionListenerSync {
    #actionDispatcherProvider;

    constructor(actionDispatcherProvider) {
        this.#actionDispatcherProvider = actionDispatcherProvider;
    }

    async handleEvent(event) {
        const dispatcherInfo = await this.#actionDispatcherProvider.getDispatcherInfo(event);
        dispatcherInfo.dispatcher.DispatchAction(dispatcherInfo.vmKey, dispatcherInfo.actionKey, dispatcherInfo.currentActionArgs);
    }
}

class ActionListenerAsync {
    #actionDispatcherProvider;

    constructor(actionDispatcherProvider) {
        this.#actionDispatcherProvider = actionDispatcherProvider;
    }

    async handleEvent(event) {
        const dispatcherInfo = await this.#actionDispatcherProvider.getDispatcherInfo(event);
        await dispatcherInfo.dispatcher.DispatchActionAsync(dispatcherInfo.vmKey, dispatcherInfo.actionKey, dispatcherInfo.currentActionArgs);
    }
}

globalThis.vitraux = {
    config: {
        useShadowDom: true,

        configure(useShadowDom) {
            this.useShadowDom = useShadowDom;
        }
    },

    getFullVMKey(vmKey) {
        return vmUpdateFunctionKeyPrefix + vmKey;
    },

    storedElements: {
        elements: {},
        getElementByIdAsArray(id) {
            const element = document.getElementById(id);

            return element ? [element] : [];
        },

        getStoredElementByIdAsArray(id, elementObjectName) {
            const elementArray = this.elements[elementObjectName]
                ?? this.storeElementByIdAsArray(id, elementObjectName);

            return elementArray;
        },

        storeElementByIdAsArray(id, elementObjectName) {
            const elementArray = this.getElementByIdAsArray(id);
            this.elements[elementObjectName] = elementArray;

            return elementArray;
        },

        getElementsByQuerySelector(parent, querySelector) {
            return parent.querySelectorAll(querySelector);
        },

        getStoredElementsByQuerySelector(parentObj, querySelector, elementsObjectName) {
            const elements = this.elements[elementsObjectName]
                ?? this.storeElementsByQuerySelector(parentObj, querySelector, elementsObjectName);

            return elements;
        },

        storeElementsByQuerySelector(parentObj, querySelector, elementsObjectName) {
            const elements = this.getElementsByQuerySelector(parentObj, querySelector);
            this.elements[elementsObjectName] = elements;

            return elements;
        },

        getTemplate(id) {
            return this.trimTemplateContent(document.getElementById(id)?.content);
        },

        getStoredTemplate(id, elementsObjectName) {
            const template = this.elements[elementsObjectName]
                ?? this.storeTemplate(id, elementsObjectName);

            return template;
        },

        storeTemplate(id, elementsObjectName) {
            const template = this.getTemplate(id);
            this.elements[elementsObjectName] = template;

            return template;
        },

        async fetchElement(uri) {
            const response = await fetch(uri);
            const html = await response.text();

            const template = document.createElement("template");
            template.innerHTML = html.trim();

            return this.trimTemplateContent(template?.content);
        },

        async getFetchedElement(uri, elementsObjectName) {
            const element = this.elements[elementsObjectName]
                ?? await this.storeFetchedElement(uri, elementsObjectName);

            return element;
        },

        async storeFetchedElement(uri, elementsObjectName) {
            const element = await this.fetchElement(uri);
            this.elements[elementsObjectName] = element;

            return element;
        },

        trimTemplateContent(templateContent) {
            if (!templateContent)
                return templateContent;

            for (let i = 0; this.tryRemoveEmptyTextNode(templateContent, i); i++);
            for (let i = templateContent.childNodes.length - 1; this.tryRemoveEmptyTextNode(templateContent, i); i--);

            return templateContent;
        },

        tryRemoveEmptyTextNode(templateContent, i) {
            const node = templateContent.childNodes[i];

            if (node && node.nodeType === Node.TEXT_NODE && !node.textContent.trim()) {
                templateContent.removeChild(node);
                return true;
            }

            return false;
        }
    },
    updating: {
        utils: {
            isValueValid(v) {
                return v || v === 0 || v === false || v === "";
            }
        },

        vmFunctions: {
            vms: [],

            async executeInitializationView(code) {
                const func = new Function(code);
                await func();
            },

            async executeUpdateViewFunctionFromJson(vmKey, vmJson) {
                const vm = JSON.parse(vmJson);
                await this.executeUpdateViewFunction(vmKey, vm);
            },

            async executeUpdateViewFunction(vmKey, vm) {
                const func = this.vms[vmKey];
                await func(vm);
            },

            getFunctionsCodeFromVersion(vmKey, version) {
                if (!vmKey || !version)
                    throw new VitrauxInternalError("vmKey and version must be set in getFunctionsCodeFromVersion!");

                const vmFuncObjJson = localStorage.getItem(globalThis.vitraux.getFullVMKey(vmKey))

                if (!vmFuncObjJson)
                    return false;

                const vmFuncObj = JSON.parse(vmFuncObjJson);

                return (vmFuncObj.version === version) ? vmFuncObj : false;
            },

            async tryInitializeViewFunctionsFromCacheByVersion(vmKey, version) {
                if (!version)
                    throw new VitrauxInternalError("Version must be set in tryInitializeViewFunctionsFromCacheByVersion!");

                const functionCodes = this.getFunctionsCodeFromVersion(vmKey, version);

                if (!functionCodes)
                    return false;

                await this.executeInitializationView(functionCodes.initializationCode);
                this.createUpdateViewFunction(vmKey, functionCodes.updateViewCode);

                return true;
            },

            async initializeNewViewFunctionsToCacheByVersion(vmKey, version, initializationCode, updateViewCode) {
                if (!version)
                    throw new VitrauxInternalError("Version must be set in initializeNewViewFunctionsToCacheByVersion!");

                await this.executeInitializationView(initializationCode);
                this.createUpdateViewFunction(vmKey, updateViewCode);

                this.storeFunctions(vmKey, version, initializationCode, updateViewCode);
            },

            async initializeNonCachedViewFunctions(vmKey, initializationCode, updateViewCode) {
                await this.executeInitializationView(initializationCode);
                this.createUpdateViewFunction(vmKey, updateViewCode);
            },

            createUpdateViewFunction(vmKey, code) {
                var allCode = "return (async () => {" + code + "})()";
                this.vms[vmKey] = new Function("vm", allCode);
            },

            storeFunctions(vmKey, version, initializationCode, updateViewCode) {
                const vmFuncObj = {
                    initializationCode: initializationCode,
                    updateViewCode: updateViewCode,
                    version: version
                };

                localStorage.setItem(globalThis.vitraux.getFullVMKey(vmKey), JSON.stringify(vmFuncObj));
            }
        },
        dom: {
            setElementsContent(elements, content) {
                for (const element of elements)
                    element.textContent = content;
            },

            setElementsHtml(elements, html) {
                for (const element of elements)
                    element.innerHTML = html;
            },

            setElementsAttribute(elements, attribute, value) {
                const setAttributeFunc = (typeof value === "boolean")
                    ? this.toggleBoolAttribute
                    : this.setAttributeValue;

                setAttributeFunc(elements, attribute, value);
            },

            setAttributeValue(elements, attribute, value) {
                for (const element of elements)
                    element.setAttribute(attribute, value);
            },

            toggleBoolAttribute(elements, attribute, value) {
                for (const element of elements)
                    element.toggleAttribute(attribute, value);
            },

            updateValueByInsertingElements(elementToInsert, appendToElements, queryChildrenFunction, updateChildElementsFunction) {
                if (!elementToInsert)
                    return;

                for (const appendToElement of appendToElements) {
                    const clonedElement = elementToInsert.cloneNode(true);
                    const targetChildElements = queryChildrenFunction(clonedElement);
                    updateChildElementsFunction(targetChildElements);

                    this.appendOnlyChild(appendToElement, clonedElement);
                }
            },

            async updateTable(tables, tbodyIndex, rowToInsert, updateCallback, collection) {
                for (const table of tables) {
                    const newTbody = document.createElement("tbody");

                    for (const collectionItem of collection) {
                        await this.addNewRow(newTbody, rowToInsert, updateCallback, collectionItem);
                    }

                    table.tBodies[tbodyIndex].replaceWith(newTbody);
                }
            },

            async updateCollectionByPopulatingElements(appendToElements, elementToInsert, updateCallback, collection) {
                for (const appendToElement of appendToElements) {

                    const newElements = [];

                    for (const collectionItem of collection) {
                        const newElement = await this.updateCollectionElement(elementToInsert, updateCallback, collectionItem);
                        newElements.push(newElement);
                    }

                    this.replaceChildren(appendToElement, newElements);
                }
            },

            async addNewRow(tbody, row, updateCallback, collectionItem) {
                const newElement = await this.updateCollectionElement(row, updateCallback, collectionItem)
                tbody.appendChild(newElement);
            },

            async updateCollectionElement(elementToInsert, updateCallback, collectionItem) {
                const clonedElement = elementToInsert.cloneNode(true);
                await updateCallback(clonedElement, collectionItem);

                return clonedElement;
            },

            replaceChildren(parentElement, childElements) {
                const rootElement = this.tryAttachShadow(parentElement);
                rootElement.replaceChildren(...childElements);
            },

            appendOnlyChild(parentElement, childElement) {
                this.replaceChildren(parentElement, [childElement]);
            },

            tryAttachShadow(element) {
                return this.supportShadowDom(element)
                    ? this.getAttachedShadow(element)
                    : element;
            },

            getAttachedShadow(element) {
                return (!element.shadowRoot)
                    ? element.attachShadow({ mode: "open" })
                    : element.shadowRoot;
            },

            supportShadowDom(element) {
                if (!globalThis.vitraux.config.useShadowDom)
                    return false;

                if (!element || element.nodeType !== Node.ELEMENT_NODE)
                    return false;

                if (element.shadowRoot)
                    return true;

                const supportedTagNames = new Set([
                    "article",
                    "aside",
                    "blockquote",
                    "body",
                    "div",
                    "footer",
                    "h1",
                    "h2",
                    "h3",
                    "h4",
                    "h5",
                    "h6",
                    "header",
                    "main",
                    "nav",
                    "p",
                    "section",
                    "span"]);

                const tag = element.tagName.toLowerCase();

                return supportedTagNames.has(tag) || (tag.includes('-') && customElements.get(tag));
            }
        }
    },
    actions: {
        dom: {
            getElementsContent(elements) {
                return elements.map(e => e.textContent);
            },

            getElementsAttribute(elements, attribute) {
                return elements.map(e => getAttributeValue(e, attribute));

                function getAttributeValue(element, attributeName) {
                    const attributeValue = element.getAttribute(attributeName);
                    return attributeName === attributeValue ? true : attributeValue;
                }
            },

            getInputsValue(inputs) {
                return inputs.map(i => i.value);
            }
        },

        registration: {
            vmActions: [],

            actionRegistrationStrategy: Object.freeze({
                OnlyOnceAtStart: "OnlyOnceAtStart",
                OnlyOnceOnDemand: "OnlyOnceOnFirstViewModelRendering",
                Always: "AlwaysOnViewModelRendering"
            }),

            getActionFullVMCacheKey(vmKey) {
                return `${globalThis.vitraux.getFullVMKey(vmKey)}-actions`;
            },

            executeActionRegistrationsCode(code) {
                const func = new Function(code);
                func();
            },

            createActionRegistrationsFunction(vmKey, code) {
                this.vmActions[vmKey] = new Function(code);
            },

            executeActionRegistrationsFunction(vmKey) {
                const func = this.vmActions[vmKey];
                func();
            },

            registerActionAsync(elements, events, vmKey, actionKey, actionArgsCallback) {
                this.addActionListenerToElements(elements, events, vmKey, actionKey, actionArgsCallback, (provider) => new ActionListenerAsync(provider));
            },

            registerActionSync(elements, events, vmKey, actionKey, actionArgsCallback) {
                this.addActionListenerToElements(elements, events, vmKey, actionKey, actionArgsCallback, (provider) => new ActionListenerSync(provider));
            },

            addActionListenerToElements(elements, events, vmKey, actionKey, actionArgsCallback, actionListenerCreatorCallback) {
                const provider = new ActionDispatcherProvider(vmKey, actionKey, actionArgsCallback);
                const actionListener = actionListenerCreatorCallback(provider);

                for (const element of elements) {
                    for (const event of events) {
                        element.addEventListener(event, actionListener);
                    }
                }
            },

            getActionFunctionCodeFromVersion(vmActionCacheKey, version) {
                if (!vmActionCacheKey || !version)
                    throw new VitrauxInternalError("vmKey and version must be set in getActionFunctionCodeFromVersion!");

                const vmActionObjJson = localStorage.getItem(vmActionCacheKey);

                if (!vmActionObjJson)
                    return false;

                const vmActionObj = JSON.parse(vmActionObjJson);

                return (vmActionObj.version === version) ? vmActionObj : false;
            },

            storeActionsFunction(vmActionKey, version, actionsCode, actionRegistrationStrategy) {
                const vmActionObj = {
                    actionsCode: actionsCode,
                    actionRegistrationStrategy: actionRegistrationStrategy,
                    version: version
                };

                localStorage.setItem(vmActionKey, JSON.stringify(vmActionObj));
            },

            tryInitializeActionsFunctionFromCacheByVersion(vmKey, version) {
                if (!vmKey || !version)
                    throw new VitrauxInternalError("Version must be set in tryInitializeActionsFunctionFromCacheByVersion!");

                const vmActionCacheKey = this.getActionFullVMCacheKey(vmKey);
                const vmActionObj = this.getActionFunctionCodeFromVersion(vmActionCacheKey, version);

                if (!vmActionObj)
                    return false;

                this.initializeActionRegistrationsByActionRegistrationStrategy(vmKey, vmActionObj.actionsCode, vmActionObj.actionRegistrationStrategy);

                return true;
            },

            initializeNonCachedActionsFunction(vmKey, actionsCode, actionRegistrationStrategy) {
                this.initializeActionRegistrationsByActionRegistrationStrategy(vmKey, actionsCode, actionRegistrationStrategy);
            },

            initializeNewActionsFunctionToCacheByVersion(vmKey, version, actionsCode, actionRegistrationStrategy) {
                if (!vmKey || !version)
                    throw new VitrauxInternalError("Version must be set in initializeNewActionsFunctionToCacheByVersion!");

                this.initializeActionRegistrationsByActionRegistrationStrategy(vmKey, actionsCode, actionRegistrationStrategy);

                const vmActionKey = this.getActionFullVMCacheKey(vmKey);
                this.storeActionsFunction(vmActionKey, version, actionsCode, actionRegistrationStrategy);
            },

            initializeActionRegistrationsByActionRegistrationStrategy(vmKey, actionsCode, actionRegistrationStrategy) {
                if (actionRegistrationStrategy === this.actionRegistrationStrategy.OnlyOnceAtStart) {
                    this.executeActionRegistrationsCode(actionsCode);
                }
                else {
                    this.createActionRegistrationsFunction(vmKey, actionsCode);
                }
            }
        }
    }
};
