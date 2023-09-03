import { Injectable } from "@angular/core";
import { GroupScheduleExams, ScheduleExamResponse } from "../models/scheduleExam.model";

@Injectable({
    providedIn: "root",
})
  
export class ExamScheduleMapperService {
    constructor() { }

    mapScheduleExamsResponseToGroupScheduleExams(scheduleExamsResponse: ScheduleExamResponse[]): GroupScheduleExams[] {
        let groupScheduleExams: GroupScheduleExams[] = [];

        scheduleExamsResponse.forEach(ser => {
            let groupScheduleExam = groupScheduleExams.filter(gse => gse.date == this.getOnlyDate(ser.date))?.at(0);

            if (!groupScheduleExam) {
                groupScheduleExams.push({
                    date: this.getOnlyDate(ser.date),
                    exams: [
                        {
                            ...ser,
                        }
                    ]
                })
            } else {
                groupScheduleExam.exams.push({
                    ...ser
                });
            }
        });

        return groupScheduleExams;
    }

    getOnlyDate(date: Date): Date {
        const properDate = new Date(date);
        return new Date(properDate.getFullYear(), properDate.getMonth(), properDate.getDate());
    }
}