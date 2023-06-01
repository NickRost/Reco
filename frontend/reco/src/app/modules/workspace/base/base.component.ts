import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { CustomIconService } from 'src/app/services/custom-icon.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss'],
})
export class BaseComponent {
  public currentUser: UserDto = {} as UserDto;
  private unsubscribe$ = new Subject<void>();
  public isShared: boolean = false;

  constructor(
    private customService: CustomIconService,
    private registrationService: RegistrationService,
    private route: ActivatedRoute
  ) {
    this.customService.init();
    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isShared = true;
      } else {
        this.isShared = false;
      }
    });
    this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        if (user.avatarLink === null) {
          user.avatarLink = '../../assets/icons/test-user-logo.png';
        }
        this.currentUser = user;
      });
  }
}
