import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/main/services/authGuard.service';
import { AuthTeacherGuard } from './core/main/services/authTeacherGuard.service';
import { ExamResultComponent } from './pages/exams-results/components/exam-result/exam-result.component';
import { ExamsResultsComponent } from './pages/exams-results/components/exams-results.component';
import { AddExamComponent } from './pages/exams/components/add-exam/add-exam.component';
import { ExamComponent } from './pages/exams/components/exam/exam.component';
import { ExamsComponent } from './pages/exams/components/exams.component';
import { AddStudentComponent } from './pages/students/components/add-student/add-student.component';
import { MembersComponent } from './pages/students/components/members.component';

const routes: Routes = [
  { path: "", redirectTo: "/exams", pathMatch: "full" },
  { path: "exams", component: ExamsComponent, canActivate: [AuthTeacherGuard] },
  { path: "exam", component: ExamComponent, canActivate: [AuthTeacherGuard]},
  { path: "add-exam", component: AddExamComponent, canActivate: [AuthTeacherGuard] },
  { path: "exams-results", component: ExamsResultsComponent },
  { path: "exam-result", component: ExamResultComponent },
  { path: "members", component: MembersComponent, canActivate: [AuthGuard] },
  { path: "add-student", component: AddStudentComponent },
 // { path: "**", component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
