namespace Vitraux.Test.JsCodeGeneration;
internal static class ExpectedGeneratedJsActionForOnlyOnceOnDemand
{
    internal const string ExpectedJsCode =
        """
        'use strict';

        const i0 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el1');

        globalThis.vitraux.actions.registration.registerActionSync(i0, ['event1'], 'vm_test', 'ak0');

        const i1 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el2');
        const i2 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el3');

        globalThis.vitraux.actions.registration.registerActionSync(i1, ['event2'], 'vm_test', 'ak1');
        globalThis.vitraux.actions.registration.registerActionSync(i2, ['event3','event4'], 'vm_test', 'ak1');

        const i3 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el4');

        globalThis.vitraux.actions.registration.registerActionAsync(i3, ['event5'], 'vm_test', 'ak2');

        const i4 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el5');
        const i5 = globalThis.vitraux.storedElements.getElementByIdAsArray('el6');

        globalThis.vitraux.actions.registration.registerActionAsync(i4, ['event6','event7'], 'vm_test', 'ak3');
        globalThis.vitraux.actions.registration.registerActionAsync(i5, ['event8','event9'], 'vm_test', 'ak3');

        const i6 = globalThis.vitraux.storedElements.getElementByIdAsArray('el7');

        const pc0 = (event) => {

            const args = {
            };

            args.inputValue = [event.target.value];

            return args;
        };

        globalThis.vitraux.actions.registration.registerParametrizableActionSync(i6, ['event10'], 'vm_test', 'ak4', pc0);

        const i7 = globalThis.vitraux.storedElements.getElementByIdAsArray('el8');

        const pc1 = (event) => {

            const p12 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'el9', 'p12');
            const p13 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('el10', 'p13');

            const args = {
                'p1': globalThis.vitraux.actions.dom.getInputsValue(p12),
                'p2': globalThis.vitraux.actions.dom.getElementsContent(p13)
            };

            args.inputValue = [event.target.value];

            return args;
        };

        globalThis.vitraux.actions.registration.registerParametrizableActionAsync(i7, ['event11'], 'vm_test', 'ak5', pc1);

        const i8 = globalThis.vitraux.storedElements.getElementByIdAsArray('el11');
        const i9 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el12');

        const pc2 = (event) => {

            const args = {
            };

            args.inputValue = [event.target.value];

            return args;
        };

        globalThis.vitraux.actions.registration.registerParametrizableActionAsync(i8, ['event12'], 'vm_test', 'ak6', pc2);
        globalThis.vitraux.actions.registration.registerParametrizableActionAsync(i9, ['event13','event14'], 'vm_test', 'ak6', pc2);

        const i10 = globalThis.vitraux.storedElements.getElementByIdAsArray('el13');
        const i11 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el14');

        const pc3 = (event) => {

            const p14 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('el15', 'p14');
            const p15 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'el16', 'p15');

            const args = {
                'p3': globalThis.vitraux.actions.dom.getInputsValue(p14),
                'p4': globalThis.vitraux.actions.dom.getElementsAttribute(p15,'att1')
            };

            args.inputValue = [event.target.value];

            return args;
        };

        globalThis.vitraux.actions.registration.registerParametrizableActionSync(i10, ['event15'], 'vm_test', 'ak7', pc3);
        globalThis.vitraux.actions.registration.registerParametrizableActionSync(i11, ['event16','event17'], 'vm_test', 'ak7', pc3);
        """;
}
