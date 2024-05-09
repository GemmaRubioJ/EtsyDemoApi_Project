import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-success-dialog',
  templateUrl: './success-dialog-component.component.html',
  styleUrls: ['./success-dialog-component.component.css']
})
export class SuccessDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string }) { }
}
