import { ConfigurationItemType } from "./enums/configurationItemType.enum";

export interface GridItemConfiguration {
    displayName: string,
    type?: ConfigurationItemType,
    key: string,
    enum?: any,
    nestedKey?: string,
    styles?: string,
    rowStyles?: string,
}