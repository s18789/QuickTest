import { Injectable } from "@angular/core";
import { CalendarExam } from "../models/calendarExam.model";
import { CalendarExamResponse } from "../models/calendarExamResponse.model";

@Injectable({
    providedIn: "root",
  })
  
export class CalendarExamMapperService {
    constructor() { }

    mapCalendarExamResponseCalendarExam(calendarExamResponse: CalendarExamResponse): CalendarExam {
        return {
            ...calendarExamResponse,
            dayOfMonth: calendarExamResponse.dayOfMonth - 1,
        };
    }
}