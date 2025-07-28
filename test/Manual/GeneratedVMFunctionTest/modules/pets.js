
export const pets = {
    manage: async (pets) => {
        console.log(`manage (from pets.js) called with pets: ${pets.map(p => p.v0).join(', ')}`);
        return Promise.resolve();
    }
};