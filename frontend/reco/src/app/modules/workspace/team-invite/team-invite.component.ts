import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-team-invite',
  templateUrl: './team-invite.component.html',
  styleUrls: ['./team-invite.component.scss'],
})
export class TeamInviteComponent implements OnInit {
  public inviteForm: FormGroup = {} as FormGroup;
  public email: string = '';

  constructor(
    private dialogRef: MatDialogRef<TeamInviteComponent>,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private snackbarService: SnackBarService
  ) {}

  ngOnInit() {
    this.inviteForm = this.formBuilder.group({
      email: [
        ,
        [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
        ],
      ],
    });
  }

  sendInvite() {
    this.userService.sendInviteLink(this.inviteForm.value.email).subscribe({
      next: () => {
        this.snackbarService.openSnackBar('Invitations sent successfully');
        this.close();
      },
      error: () => {
        this.snackbarService.openSnackBar('User is not found');
      },
    });
  }

  close() {
    this.dialogRef.close();
  }
}
