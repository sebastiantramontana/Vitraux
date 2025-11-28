namespace Vitraux.Test.JsCodeGeneration;
internal static class ExpectedGeneratedJsActionForAlways
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

            const p15 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el9');
            const p16 = globalThis.vitraux.storedElements.getElementByIdAsArray('el10');

            const args = {
                'p1': globalThis.vitraux.actions.dom.getInputsValue(p15),
                'p2': globalThis.vitraux.actions.dom.getElementsContent(p16)
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

            const p17 = globalThis.vitraux.storedElements.getElementByIdAsArray('el15');
            const p18 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el16');

            const args = {
                'p3': globalThis.vitraux.actions.dom.getInputsValue(p17),
                'p4': globalThis.vitraux.actions.dom.getElementsAttribute(p18,'att1')
            };

            args.inputValue = [event.target.value];

            return args;
        };

        globalThis.vitraux.actions.registration.registerParametrizableActionSync(i10, ['event15'], 'vm_test', 'ak7', pc3);
        globalThis.vitraux.actions.registration.registerParametrizableActionSync(i11, ['event16','event17'], 'vm_test', 'ak7', pc3);

        const i12 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el17');

        const pc4 = (event) => {

            const p19 = globalThis.vitraux.storedElements.getElementByIdAsArray('el18');
            const p20 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el19');

            const args = {
                'p5': globalThis.vitraux.actions.dom.getInputsValue(p19),
                'p6': globalThis.vitraux.actions.dom.getElementsContent(p20)
            };

            return args;
        };

        globalThis.vitraux.actions.registration.registerParametrizableActionAsync(i12, ['event18','event19'], 'vm_test', 'ak8', pc4);

        const i13 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el20');
        const i14 = globalThis.vitraux.storedElements.getElementByIdAsArray('el21');

        const pc5 = (event) => {

            const p21 = globalThis.vitraux.storedElements.getElementByIdAsArray('el22');
            const p22 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, 'el23');

            const args = {
                'p7': globalThis.vitraux.actions.dom.getInputsValue(p21),
                'p8': globalThis.vitraux.actions.dom.getElementsAttribute(p22,'att2')
            };

            return args;
        };

        globalThis.vitraux.actions.registration.registerParametrizableActionSync(i13, ['event20','event21'], 'vm_test', 'ak9', pc5);
        globalThis.vitraux.actions.registration.registerParametrizableActionSync(i14, ['event22'], 'vm_test', 'ak9', pc5);
        """;
}
