import { ExistingRecordsSummary, ImportedGroupsDto } from './importSchoolDataResponse.model';

export interface ImportSummaryDto {
  ExistingTeachers: number;
  ExistingStudents: number;
  RecordsSummary: ExistingRecordsSummary;
  ImportedGroupsFromFile: ImportedGroupsDto;
}