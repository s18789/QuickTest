import { ImportSummaryDto } from './import-summary.model'; 

export interface BulkImportRequest {
  ImportSummary: ImportSummaryDto;
  SchoolId: number;
}