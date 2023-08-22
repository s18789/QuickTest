import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteUrls } from './shared/enums/route-urls.enum';

const routes: Routes = [
  {
    
    path: "",
    children: [
      { path: '', 
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
    ],
  },
  // { path: "", redirectTo: "/dashboard", pathMatch: "full" },
  // { path: "dashboard", component: DashboardComponent, canActivate: [AuthTeacherGuard]},
  // { path: "exams", component: ExamsComponent, canActivate: [AuthTeacherGuard] },
  // { path: "exam/:id", component: ExamComponent, canActivate: [AuthTeacherGuard]},
  // { path: "add-exam", component: AddExamComponent, canActivate: [AuthTeacherGuard] },
  // { path: "exams-results/:id", component: ExamsResultsComponent },
  // { path: "exams-results", component: ExamsResultsComponent },
  // { path: "exam-result/:id", component: ExamResultComponent },
  // { path: "members", component: MembersComponent, canActivate: [AuthGuard] },
  // { path: "add-student", component: AddStudentComponent },
  // { path: "**", component: PageNotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
