import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PersonalComponent } from '../../personal/personal.component';

@Component({
  selector: 'app-update-video-dialog',
  templateUrl: './update-video-dialog.component.html',
  styleUrls: ['./update-video-dialog.component.scss']
})
export class UpdateVideoDialogComponent implements OnInit {

  form: FormGroup = {} as FormGroup;
  videoName : string = '';

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<PersonalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string) {
      this.videoName = data;
     }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      videoName: [, {
        validators: [
          Validators.required,
          Validators.maxLength(30),
        ],
        updateOn: 'change',
      }],
    });
    this.form.get('videoName')?.setValue(this.videoName)
  }

  save() {
    this.dialogRef.close(this.form.value.videoName);
  }

}
