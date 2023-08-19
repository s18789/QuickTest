export interface PricingPlan {
    title: string,
    description: string,
    price: number,
    advantages: PricingPlanAnwantage[],
}

export interface PricingPlanAnwantage {
    title: string
}