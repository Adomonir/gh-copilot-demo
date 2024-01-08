import {describe, it} from 'mocha';
import {expect} from 'chai';

import {validateDate, validateIPV6} from '../utils/validators';

describe('validateDate', () => {
    it('should return a valid date object for a valid input', () => {
        const input = '31/12/2022';
        const result = validateDate(input);
        expect(result).to.be.an.instanceOf(Date);
        expect(result.getFullYear()).to.equal(2022);
        expect(result.getMonth()).to.equal(11); // Months are zero-based
        expect(result.getDate()).to.equal(31);
    });
// throws and error when given en empty string.
    it('should throw an error for an empty input', () => {
        const input = '';
        expect(() => validateDate(input)).to.throw('Invalid input');
    });
});

