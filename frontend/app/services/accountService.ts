import anonymousClient from '../clients/anonymouslClient';
import { LoginModel } from '../models/loginModel';

export async function loginUserOnServer(loginModel: LoginModel) {
    const response = await anonymousClient.post('account/token', loginModel);
    return response;
}