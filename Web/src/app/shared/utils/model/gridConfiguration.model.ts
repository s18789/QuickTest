import { ConfigurationItemType } from "./enums/configurationItemType.enum";

export interface GridItemConfiguration {
    displayName: string,
    type?: ConfigurationItemType,
    key: string,
    nestedKey?: string,
    styles?: string,
    rowStyles?: string,
}