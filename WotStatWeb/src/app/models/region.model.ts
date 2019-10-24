export class Region {
    name: string;
    urlSuffix: string;
}

export const AvailableRegions: Region[] = [
    { 
        name: 'Europe',
        urlSuffix: 'eu',
    },
    { 
        name: 'North America',
        urlSuffix: 'com',
    },
    { 
        name: 'Asia',
        urlSuffix: 'asia',
    },
    { 
        name: 'Russia',
        urlSuffix: 'ru',
    },
]