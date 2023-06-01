import { Component, Input, OnChanges } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { TeamDto } from 'src/app/models/user/team-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { TeamInviteComponent } from '../team-invite/team-invite.component';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnChanges {
  @Input() public user: UserDto = {} as UserDto;
  selectedCriteria: TeamDto | undefined;
  members: number | undefined;
  value: string = '';

  constructor(public dialog: MatDialog, private userService: UserService) {}

  ngOnChanges() {
    this.selectedCriteria = this.user.teams.filter(
      (t) => t.authorId === this.user.id
    )[0];

    this.value = this.selectedCriteria.name;
    this.initValues();
  }

  initValues() {
    this.members = this.selectedCriteria?.memberCount;
  }

  onChange() {
    this.selectedCriteria = this.user.teams.filter(
      (t) => t.name === this.value
    )[0];

    this.value = this.selectedCriteria.name;
    this.initValues();
  }

  showInvite() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    this.dialog.open(TeamInviteComponent, dialogConfig);
  }
}
