import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit} from '@angular/core';
import { NgForm } from '@angular/forms';
import { map, Observable, shareReplay } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { ExamSolveService } from '../services/examSolveService.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit{
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(
    private breakpointObserver: BreakpointObserver,
    public authService: AuthService,
    public examSolveService: ExamSolveService
    ) {}

  ngOnInit(): void {
  }

  onLogOut(){
    this.authService.logOut();
  }
}
