import { TokenDto } from '../token/token-dto';
import { UserDto } from './user-dto';

export interface AuthUserDto {
    user: UserDto;
    token: TokenDto;
}