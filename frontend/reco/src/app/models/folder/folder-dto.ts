import { UserDto } from '../user/user-dto';

export interface FolderDto{
  id:number;
  name:string;
  authorId:number;
  author:UserDto;
  teamId:number | undefined;
}