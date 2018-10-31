process.env.TS_NODE_PROJECT = './tsconfig.json';
require('ts-mocha');
const Mocha = require('mocha');

const mocha = new Mocha();
mocha.addFile(`./token-expiration-spec/token-expiration-basic.spec.ts`);
mocha.run((failures) => {
    process.on('exit', () => {
        process.exit(failures); // exit with non-zero status if there were failures
    });
});
