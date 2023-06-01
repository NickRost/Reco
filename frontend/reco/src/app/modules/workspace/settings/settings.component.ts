import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import jwt_decode from 'jwt-decode';
import { UserUpdateDto } from 'src/app/models/user/user-update-dto';
import { LoginService } from 'src/app/services/login.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UserService } from 'src/app/services/user.service';
import { environment } from 'src/environments/environment';
import {
  cannotContainSpace,
  startsOrEndWithSpace,
} from 'src/app/core/validators/customValidators';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogDeleteComponent } from '../dialog-delete/dialog-delete.component';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit {
  private readonly APIUrl = environment.apiUrl;
  public formAvatar: FormGroup = {} as FormGroup;
  public formPassword: FormGroup = {} as FormGroup;
  public avatar!: string;

  public oldName: string = '';
  public oldEmail: string = '';
  public userId: string = '';
  public name: string = '';
  public imageFile!: File;
  public result: Boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private loginService: LoginService,
    private userService: UserService,
    public dialog: MatDialog,
    private snackbarService: SnackBarService
  ) {}

  ngOnInit(): void {
    this.initValues();
    this.initForms();
  }

  initValues() {
    let token = localStorage.getItem('accessToken') || '';

    if (token) {
      let decoded: { name: string; email: string; id: string } =
        jwt_decode(token);

      this.name = decoded.name;
      this.oldName = this.name;
      this.oldEmail = decoded.email;
      this.userId = decoded.id;
    }
  }

  initForms() {
    this.formAvatar = this.formBuilder.group({
      workspaceName: [
        this.name,
        {
          validators: [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(30),
            Validators.pattern(
              // prettier-ignore
              '^[а-яА-ЯёЁa-zA-Z\`\'][а-яА-ЯёЁa-zA-Z-\`\' ]+[а-яА-ЯёЁa-zA-Z\`\']?$'
            ),
            startsOrEndWithSpace,
          ],
        },
      ],
    });

    this.formPassword = this.formBuilder.group({
      email: [
        this.oldEmail,
        {
          validators: [
            Validators.required,
            Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
          ],
        },
      ],
      passOld: [
        ,
        {
          validators: [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(20),
            Validators.pattern(
              /^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9*.!@#$%^&`(){}[\]:;<>,‘.?/~_+=|-]+)$/
            ),
            cannotContainSpace,
          ],
        },
      ],
      passNew: [
        ,
        {
          validators: [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(20),
            Validators.pattern(
              /^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9*.!@#$%^&`(){}[\]:;<>,‘.?/~_+=|-]+)$/
            ),
            cannotContainSpace,
          ],
        },
      ],
    });
  }

  errorHandler(event: Event) {
    const target = event.target as HTMLInputElement;
    target.src = '../../assets/icons/test-user-logo.png';
  }

  saveNewInfo() {
    let workspaceName = this.formAvatar.controls['workspaceName'].value;

    const data = new FormData();
    data.append('id', this.userId);
    data.append('workspaceName', workspaceName);
    data.append('avatar', this.imageFile);

    this.userService.updateInfo('Update', data).subscribe({
      next: () => {
        this.snackbarService.openSnackBar('Settings saved successfully');
      },
      error: () => {
        this.snackbarService.openSnackBar(
          'Settings not saved. Something went wrong'
        );
      },
    });
  }

  onSelect(event: { addedFiles: File[] }) {
    let added = event.addedFiles;
    if (added?.length > 0) {
      this.imageFile = added[0];

      const reader = new FileReader();
      reader.addEventListener(
        'load',
        () => (this.avatar = reader.result as string)
      );
      reader.readAsDataURL(this.imageFile);
    }
  }

  saveNewInfoCancel() {
    this.formAvatar.controls['workspaceName'].setValue(this.oldName);
    this.avatar = '';
  }

  public handleFileInput(event: Event) {
    let target = event.target as HTMLInputElement;
    this.imageFile = target.files?.item(0) as File;

    if (!this.imageFile) {
      target.value = '';
      return;
    }

    if (this.imageFile.size / 1024 / 1024 > 5) {
      this.snackbarService.openSnackBar('Image can`t be heavier than ~5MB');
      target.value = '';
      return;
    }

    const reader = new FileReader();
    reader.addEventListener(
      'load',
      () => (this.avatar = reader.result as string)
    );
    reader.readAsDataURL(this.imageFile);
  }

  saveNewPass() {
    let email = this.formPassword.controls['email'].value;
    let passCurrent = this.formPassword.controls['passOld'].value;
    let passNew = this.formPassword.controls['passNew'].value;
    let userId = this.userId;

    let userUpdateDto: UserUpdateDto = {
      id: userId,
      email,
      passwordCurrent: passCurrent,
      passwordNew: passNew,
    };

    this.userService
      .updatePassword('Update-Password-Email', userUpdateDto)
      .subscribe({
        next: () => {
          this.snackbarService.openSnackBar('Settings saved successfully');
        },
        error: () => {
          this.snackbarService.openSnackBar(
            'Settings not saved. Something went wrong'
          );
        },
      });
  }

  saveNewPassCancel() {
    this.formPassword.controls['email'].setValue(this.oldEmail);
    this.formPassword.controls['passOld'].setValue('');
    this.formPassword.controls['passNew'].setValue('');
  }

  resetPassword() {
    let userId = this.userId;

    this.userService.resetPassword(userId).subscribe({
      next: () => {
        this.snackbarService.openSnackBar('Password reseted successfully');
      },
      error: () => {
        this.snackbarService.openSnackBar(
          'Password not reseted. Something went wrong'
        );
      },
    });

    return false;
  }

  deleteUser() {
    const dialogConfig = new MatDialogConfig;

    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(DialogDeleteComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((dialogResult : boolean) => {
      if (dialogResult) {
        let userId = this.userId;
        this.userService.deleteUser(`Delete-User/${userId}`).subscribe({
          next: () => {
            this.loginService.logOut();
            this.snackbarService.openSnackBar('User deleted successfully');
          },
          error: () => {
            this.snackbarService.openSnackBar(
              'User not deleted. Something went wrong'
            );
          },
        });
    }});
  }
}
