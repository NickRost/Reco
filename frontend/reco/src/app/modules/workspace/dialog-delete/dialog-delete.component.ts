import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { SettingsComponent } from '../settings/settings.component';

@Component({
  selector: 'app-dialog-delete',
  templateUrl: './dialog-delete.component.html',
  styleUrls: ['./dialog-delete.component.scss']
})
export class DialogDeleteComponent {

  constructor(
    private dialogRef: MatDialogRef<SettingsComponent>,
  ) { }

  onConfirm(): void {
    // Close the dialog, return true
    this.dialogRef.close(true);
  }
}
