import { Component, OnInit } from '@angular/core';
import { PricingPlan } from '../../models/pricing.model';

@Component({
  selector: 'app-pricing',
  templateUrl: './pricing.component.html',
  styleUrls: ['./pricing.component.css']
})
export class PricingComponent implements OnInit {
  pricingPlans: PricingPlan[];

  constructor() { }

  ngOnInit(): void {
    this.pricingPlans = [
      {
        title: "Freelancer",
        description: "The essentials to provide your best work for clients.",
        price: 24,
        advantages: [
          {
            title: "5 products"
          },
          {
            title: "Up to 1,000 subscribers"
          },
          {
            title: "Basic analytics"
          },
          {
            title: "48-hour support response time"
          }
        ]
      },
      {
        title: "Startup",
        description: "A plan that scales with your rapidly growing business.",
        price: 32,
        advantages: [
          {
            title: "25 products"
          },
          {
            title: "Up to 10,000 subscribers"
          },
          {
            title: "Advanced analytics"
          },
          {
            title: "24-hour support response time"
          },
          {
            title: "Marketing automations"
          }
        ]
      },
      {
        title: "Enterprise",
        description: "Dedicated support and infrastructure for your company.",
        price: 58,
        advantages: [
          {
            title: "Unlimited products"
          },
          {
            title: "Unlimited subscribers"
          },
          {
            title: "Advanced analytics"
          },
          {
            title: "1-hour, dedicated support response time"
          },
          {
            title: "Marketing automations"
          }
        ]
      }
    ]
  }

}
