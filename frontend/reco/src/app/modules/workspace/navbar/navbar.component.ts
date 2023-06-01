import { Component, Input, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';
import { UserDto } from 'src/app/models/user/user-dto';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  @Input() public user: UserDto = {} as UserDto
  @ViewChild(MatMenuTrigger)
  contextMenu?: MatMenuTrigger;
  constructor(private loginService: LoginService) {}
  onContextMenu(event: MouseEvent) {
    event.preventDefault();
    this.contextMenu?.menu.focusFirstItem('mouse');
    this.contextMenu?.openMenu();
  }

  onLogOut() {
    this.loginService.logOut();
  }
}
