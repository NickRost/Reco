import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BaseComponent } from './base/base.component';
import { InviteFinishComponent } from './invite-finish/invite-finish.component';
import { PersonalComponent } from './personal/personal.component';
import { SettingsComponent } from './settings/settings.component';
import { VideoPageComponent } from './video/video-page/video-page.component';

const routes: Routes = [
  {
    path: '',
    component: BaseComponent,
    children: [
      {
        path: '',
        component: PersonalComponent,
      },
      { path: 'team-invite/:token', component: InviteFinishComponent },
      {
        path: 'settings',
        component: SettingsComponent,
      },
      {
        path: '',
        redirectTo: '',
        pathMatch: 'full',
      },
      {
        path: 'video/:id',
        component: VideoPageComponent,
      },
      {
        path: 'folder/:id',
        component: PersonalComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BaseRoutingModule {}
