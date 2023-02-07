import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainComponent } from './core/main/components/main.component';
import { MaterialModule } from './material.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { GridComponent } from './shared/components/grid/grid.component';
import { ExamsComponent } from './pages/exams/components/exams.component';
import { MembersComponent } from './pages/students/components/members.component';
import { AddExamComponent } from './pages/exams/components/add-exam/add-exam.component';
import { ReactiveFormsModule } from '@angular/forms';
import { StudentDialogComponent } from './pages/exams/components/add-exam/student-dialog/student-dialog.component';
import { HeaderComponent } from './core/main/components/header/header.component';
import { FooterComponent } from './core/main/components/footer/footer.component';
import { NavigationComponent } from './core/main/components/navigation/navigation.component';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faChevronLeft, faBars, faChevronDown, faPlus, faMagnifyingGlass, faChevronRight, faArrowLeftLong, faArrowRightLong, faXmark, faCircleExclamation, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { faClock } from '@fortawesome/free-regular-svg-icons';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { AuthComponent } from './core/main/components/auth/auth.component';
import { ExamComponent } from './pages/exams/components/exam/exam.component';
import { AddStudentComponent } from './pages/students/components/add-student/add-student.component';
import { AddGroupComponent } from './pages/students/components/add-group/add-group.component';
import { GroupsComponent } from './pages/students/components/groups/groups.component';
import { StudentsComponent } from './pages/students/components/students/students.component';
import { JwtModule } from '@auth0/angular-jwt';
import { ExamsResultsComponent } from './pages/exams-results/components/exams-results.component';
import { ExamResultComponent } from './pages/exams-results/components/exam-result/exam-result.component';
import { ExamToSolveComponent } from './core/main/components/exam-to-solve/exam-to-solve.component';
import { PrepareToStartExamComponent } from './pages/exams-results/components/exam-result/ui/prepare-to-start-exam/prepare-to-start-exam.component';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    GridComponent,
    ExamsComponent,
    MembersComponent,
    AddExamComponent,
    StudentDialogComponent,
    HeaderComponent,
    FooterComponent,
    NavigationComponent,
    AuthComponent,
    ExamComponent,
    AddStudentComponent,
    AddGroupComponent,
    GroupsComponent,
    StudentsComponent,
    ExamsResultsComponent,
    ExamResultComponent,
    ExamToSolveComponent,
    PrepareToStartExamComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    AngularSvgIconModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7067"],
        //disallowedRoutesRoutes: []
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(iconLibrary: FaIconLibrary) {
    iconLibrary.addIcons(faChevronLeft, faBars, faChevronDown, faPlus, faMagnifyingGlass, faChevronRight, faArrowLeftLong, faArrowRightLong, faClock, faXmark, faCircleExclamation, faArrowRight);
  }
}
