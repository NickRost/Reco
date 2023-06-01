import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-invite-finish',
  templateUrl: './invite-finish.component.html',
  styleUrls: ['./invite-finish.component.scss'],
})
export class InviteFinishComponent implements OnInit {
  token!: string;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    protected httpClient: HttpClient,
    protected snackBarService: SnackBarService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.token = params['token'];
    });

    this.joinToTeam();
  }

  joinToTeam() {
    this.userService.addToTeam(this.token).subscribe({
      next: () => {
        this.snackBarService.openSnackBar('Successfully added to team');
      },
      error: () => {
        this.router.navigate(['/']);
        this.snackBarService.openSnackBar('Unable to add current user to team');
      },
    });
  }
}
