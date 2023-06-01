import { Component, Inject, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { PersonalComponent } from '../personal/personal.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {

  form: FormGroup = {} as FormGroup;
  folderName : string = '';

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<PersonalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string) {
      this.folderName = data;
     }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      folderName: [, {
        validators: [
          Validators.required,
          Validators.maxLength(30),
        ],
        updateOn: 'change',
      }],
    });
    this.form.get('folderName')?.setValue(this.folderName)
  }

  save() {
    this.dialogRef.close(this.form.value.folderName);
  }

}
