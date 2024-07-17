function formatNumberWithCommas(number) {
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}


function formatDateShort(date, format = 'dd/MM/yyyy') {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();

    if (format === 'dd/MM/yyyy') {
        return `${day}/${month}/${year}`;
    } else if (format === 'MM/dd/yyyy') {
        return `${month}/${day}/${year}`;
    } else {
        throw new Error('Unsupported date format');
    }
}