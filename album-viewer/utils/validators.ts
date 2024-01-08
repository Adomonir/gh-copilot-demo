// validate date from text input in french format and convert it to a date object
function validateAndConvertDate(input: string): Date | string {
    const regex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
    const match = input.match(regex);

    if (match) {
        const day = parseInt(match[1], 10);
        const month = parseInt(match[2], 10) - 1; // JavaScript counts months from 0
        const year = parseInt(match[3], 10);

        const date = new Date(year, month, day);

        // Check if date is valid (e.g., no February 30)
        if (date && date.getMonth() === month && date.getDate() === day) {
            return date;
        }
    }

    return 'Invalid date format. Please use DD/MM/YYYY.';
}

// validate the format of a GUID string
function validateGUID(guid: string): boolean {
    const regex = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
    return regex.test(guid);
}

// validate the format of an IPv6 address string
function validateIPv6Address(ipv6Address: string): boolean {
    const regex = /^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$/;
    return regex.test(ipv6Address);
}
// validate phone number from text input and extract the country code
function validateAndExtractCountryCode(phoneNumber: string): string | null {
    const regex = /^\+(?<countryCode>\d{1,3})-(?<phoneNumber>\d+)$/;
    const match = phoneNumber.match(regex);

    if (match) {
        const countryCode = match.groups?.countryCode;
        return countryCode || null;
    }

    return null;
}
