import { ConfigurationItemType } from "./enums/configurationItemType.enum";

export interface ActionConfiguration {
    display?: boolean
    propertyName?: string,
    type?: ConfigurationItemType,
    nestedPropertyName?: string,
}