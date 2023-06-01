import { TeamDto } from './team-dto';

export interface UserDto {
  id: number;
  email: string;
  workspaceName: string;
  avatarLink: string;
  teams: TeamDto[];
}
