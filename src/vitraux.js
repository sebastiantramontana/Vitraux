"use strict";

globalThis.vitraux = {

    storedElements: {
        elements: {},
        getElementByIdAsArray(id) {
            const element = document.getElementById(id);

            return element ? [element] : [];
        },

        getStoredElementByIdAsArray(id, elementObjectName) {
            const elementArray = this.elements["document"][elementObjectName]
                ?? this.storeElementByIdAsArray(id, elementObjectName);

            return elementArray;
        },

        storeElementByIdAsArray(id, elementObjectName) {
            const elementArray = this.getElementByIdAsArray(id);
            this.elements["document"][elementObjectName] = elementArray;

            return elementArray;
        },

        getElementsByQuerySelector(parent, querySelector) {
            return parent.querySelectorAll(querySelector);
        },

        getStoredElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName) {
            const elements = this.elements[parentObjName][elementsObjectName]
                ?? this.storeElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName);

            return elements;
        },

        storeElementsByQuerySelector(parentObj, parentObjName, querySelector, elementsObjectName) {
            const elements = this.getElementsByQuerySelector(parentObj, querySelector);
            this.elements[parentObjName][elementsObjectName] = elements;

            return elements;
        },

        getTemplate(id) {
            return document.getElementById(id)?.content;
        },

        getStoredTemplate(id, elementsObjectName) {
            const template = this.elements["document"][elementsObjectName]
                ?? this.storeTemplate(id, elementsObjectName);

            return template;
        },

        storeTemplate(id, elementsObjectName) {
            const template = this.getTemplate(id);
            this.elements["document"][elementsObjectName] = template;

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
            const element = this.elements["document"][elementsObjectName]
                ?? await this.storeFetchedElement(uri, elementsObjectName);

            return element;
        },

        async storeFetchedElement(uri, elementsObjectName) {
            const element = await this.fetchElement(uri);
            this.elements["document"][elementsObjectName] = element;

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

        UpdateByPopulatingElements(element, appendToElements, toChildQueryFunction, updateChildElementsFunction) {
            if (!element)
                return;

            for (const appendToElement of appendToElements) {
                const clonedElement = element.cloneNode(true);
                targetChildElements = toChildQueryFunction(clonedElement);
                updateChildElementsFunction(targetChildElements);

                vitraux.appendChild(appendToElement, clonedElement);
            }
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
