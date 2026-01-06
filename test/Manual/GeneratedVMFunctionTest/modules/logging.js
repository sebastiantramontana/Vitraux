export async function logObject(stringifiedObj) {
    console.log(`Stringified object using object.ToString(): ${stringifiedObj}`);
    return Promise.resolve();
}