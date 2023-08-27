import { Component, OnInit} from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ExamSolveService } from '../services/examSolveService.service';
import { BehaviorSubject, Subject, takeUntil, tap } from 'rxjs';
import { LoaderService } from 'src/app/shared/services/loaderService.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  isLoading$ = new BehaviorSubject<boolean>(false);
  private destroy$ = new Subject<void>();
  
  constructor(
    private loaderService: LoaderService,
    public authService: AuthService,
    public examSolveService: ExamSolveService
    ) {}

  ngOnInit(): void {

    this.loaderService.loading$
      .pipe(
        takeUntil(this.destroy$),
        tap((loadingState) => {
          this.isLoading$.next(loadingState);
        })
      )
      .subscribe();
  }

  onLogOut(){
    this.authService.logOut();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
