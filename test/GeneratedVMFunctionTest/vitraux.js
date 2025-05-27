"use strict";

globalThis.vitraux = {

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
            return document.getElementById(id)?.content;
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
            template.innerHTML = html;
            return template?.content;
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
        }
    },
    updating: {
        vms: {},
        createUpdateFunction(vmName, code) {
            const func = new Function("vm", code);

            this.vms[vmName] = {
                function: func
            };
        },

        executeUpdateFunction(vmName, vm) {
            const func = this.vms[vmName].function;
            func(vm);
        },

        setElementsContent(elements, content) {
            for (const element of elements)
                element.textContent = content;
        },

        setElementsAttribute(elements, attribute, value) {
            for (const element of elements)
                element.setAttribute(attribute, value);
        },

        UpdateValueByInsertingElements(elementToInsert, appendToElements, queryChildrenFunction, updateChildElementsFunction) {
            if (!elementToInsert)
                return;

            for (const appendToElement of appendToElements) {
                const clonedElement = elementToInsert.cloneNode(true);
                const targetChildElements = queryChildrenFunction(clonedElement);
                updateChildElementsFunction(targetChildElements);

                vitraux.appendOnlyChild(appendToElement, clonedElement);
            }
        },

        async UpdateTable(tables, rowToInsert, updateCallback, collection) {
            for (const table of tables) {
                const newTbody = document.createElement("tbody");

                for (const collectionItem of collection) {
                    await this.addNewRow(newTbody, rowToInsert, updateCallback, collectionItem);
                }

                table.tBodies[0].replaceWith(newTbody);
            }
        },

        async UpdateCollectionByPopulatingElements(appendToElements, elementToInsert, updateCallback, collection) {
            for (const appendToElement of appendToElements) {

                const newElements = [];

                for (const collectionItem of collection) {
                    const newElement = await this.updateCollectionElement(elementToInsert, updateCallback, collectionItem);
                    newElements.push(newElement);
                }

                vitraux.replaceChildren(appendToElement, newElements);
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
        }
    },

    executeCode(code) {
        const func = new Function(code);
        func();
    },

    replaceChildren(parentElement, childElements) {
        const rootElement = vitraux.tryAttachShadow(parentElement);
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
};
