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
            const parser = new DOMParser();
            const doc = parser.parseFromString(html, 'text/html');

            return doc.body.firstElementChild;
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
            for (const element in elements)
                element.textContent = content;
        },

        setElementsAttribute(elements, attribute, value) {
            for (const element in elements)
                element.setAttribute(attribute, value);
        },

        UpdateValueByInsertingElements(elementToInsert, appendToElements, queryChildrenFunction, updateChildElementsFunction) {
            if (!elementToInsert)
                return;

            for (const appendToElement of appendToElements) {
                const clonedElement = elementToInsert.cloneNode(true);
                targetChildElements = queryChildrenFunction(clonedElement);
                updateChildElementsFunction(targetChildElements);

                vitraux.appendChild(appendToElement, clonedElement);
            }
        },

        UpdateTable(tables, rowToInsert, updateCallback, collection) {
            for (const table of tables) {
                const newTbody = document.createElement("tbody");

                for (const collectionItem of collection) {
                    this.addNewRow(newTbody, rowToInsert, updateCallback, collectionItem);
                }

                table.tBodies[0].replaceWith(newTbody);
            }
        },

        UpdateCollectionByPopulatingElements(appendToElements, elementToInsert, updateCallback, collection) {
            for (const appendToElement of appendToElements) {
                for (const collectionItem of collection) {
                    this.addNewElement(appendToElement, elementToInsert, updateCallback, collectionItem);
                }
            }
        },

        addNewRow(tbody, row, updateCallback, collectionItem) {
            this.addNewElement(tbody, row, updateCallback, collectionItem);
        },

        addNewElement(appendToElement, elementToInsert, updateCallback, collectionItem) {
            const clonedElement = elementToInsert.cloneNode(true);
            updateCallback(clonedElement, collectionItem);

            appendToElement.appendChild(clonedElement);
        }
    },

    executeCode(code) {
        const func = new Function(code);
        func();
    },

    appendChild(parentElement, childElement) {
        const rootElement = this.tryAttachShadow(parentElement);
        rootElement.appendChild(childElement);
    },

    tryAttachShadow(element) {
        return this.supportShadowDom(element)
            ? element.attachShadow({ mode: "open" })
            : element;
    },

    supportShadowDom(element) {
        const supportedTagNames = [
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
            "span"];

        return supportedTagNames.some((tn) => tn === element.tagName);
    }
};
