import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteUrls } from './shared/enums/route-urls.enum';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';
import { ImportSummaryComponent } from './pages/dashboard/components/adminDashboard/components/ui/import-from-file/components/import-summary/import-summary.component';
import { CreateAccountsSummaryComponent } from './pages/dashboard/components/adminDashboard/components/ui/import-from-file/components/import-summary/create-accounts-summary/create-accounts-summary.component';

const routes: Routes = [
  {
    
    path: "",
    children: [
      { 
        path: '', 
        redirectTo: RouteUrls.Dashboard, 
        pathMatch: 'full' 
      },
      {
        //canActivate: [MsalGuard],
        path: RouteUrls.Dashboard,
        loadChildren: () =>
          import("./pages/dashboard/dashboard.module").then(
            (m) => m.DashboardModule,
          ),
      },
      {
        //canActivate: [MsalGuard],
        path: "",
        loadChildren: () =>
          import("./pages/exams/exams.module").then(
            (m) => m.ExamsModule,
          ),
      },
      {
        //canActivate: [MsalGuard],
        path: RouteUrls.ExamsResults,
        loadChildren: () =>
          import("./pages/exams-results/exams-results.module").then(
            (m) => m.ExamsResultsModule,
          ),
      },
      {
        //canActivate: [MsalGuard],
        path: RouteUrls.Members,
        loadChildren: () =>
          import("./pages/members/members.module").then(
            (m) => m.MembersModule,
          ),
      },
      {
        //canActivate: [MsalGuard],
        path: RouteUrls.Pricing,
        loadChildren: () =>
          import("./pages/pricing/pricing.module").then(
            (m) => m.PricingModule,
          ),
      },
      {
        //canActivate: [MsalGuard],
        path: RouteUrls.Teachers,
        loadChildren: () =>
          import("./pages/teachers/teachers.module").then(
            (m) => m.TeachersModule,
          ),
      },
      {
        //canActivate: [MsalGuard],
        path: RouteUrls.Calendar,
        loadChildren: () =>
          import("./pages/calendar/calendar.module").then(
            (m) => m.CalendarModule,
          ),
      },
      {
        //canActivate: [MsalGuard],
        path: `${RouteUrls.ImportSummary}/:importId`,
        component: ImportSummaryComponent
      },
      {
        //canActivate: [MsalGuard],
        path: 'create-accounts-summary', 
        component: CreateAccountsSummaryComponent
      },
      {
        path: "**",
        component: PageNotFoundComponent
      },
    ],
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
