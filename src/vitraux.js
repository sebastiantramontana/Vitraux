"use strict";

globalThis.vitraux = {

    storedElements: {
        elements: {},
        getElementByIdAsArray(parent, id) {
            const element = parent.getElementById
                ? parent.getElementById(id)
                : parent.querySelector("#".concat(id));

            return element ? [element] : [];
        },

        getStoredElementByIdAsArray(parentObj, parentObjName, id, elementObjectName) {
            const elementArray = this.elements[parentObjName][elementObjectName]
                ?? this.storeElementByIdAsArray(parentObj, parentObjName, id, elementObjectName);

            return elementArray;
        },

        storeElementByIdAsArray(parentObj, parentObjName, id, elementObjectName) {
            const elementArray = this.getElementByIdAsArray(parentObj, id);
            this.elements[parentObjName][elementObjectName] = elementArray;

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

        getElementByTemplateAsArray(id) {
            return document.getElementById(id)?.content ?? [];
        },

        getStoredElementByTemplateAsArray(id, elementsObjectName) {
            const elements = this.elements["document"][elementsObjectName]
                ?? this.storeElementByTemplateAsArray(id, elementsObjectName);

            return elements;
        },

        storeElementByTemplateAsArray(id, elementsObjectName) {
            const element = this.getElementByTemplateAsArray(id);
            this.elements["document"][elementsObjectName] = element;

            return element;
        },
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

        UpdateByTemplate(templateContent, appendToElements, toChildQueryFunction, updateTemplateChildFunction) {
            for (const appendToElement of appendToElements) {
                const clonedTemplateContent = templateContent.cloneNode(true);
                targetTemplateChildElements = toChildQueryFunction(clonedTemplateContent);
                updateTemplateChildFunction(targetTemplateChildElements);

                vitraux.appendChild(appendToElement, clonedTemplateContent);
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
