import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-user-exists-dialog',
  templateUrl: './user-exist-dialog.component.html',
  styleUrls: ['./user-exist-dialog.component.css']
})
export class UserExistsDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string }) { }
}
