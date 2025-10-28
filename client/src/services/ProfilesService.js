import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { Account } from "@/models/Account.js";
import { logger } from "@/utils/Logger.js";

class ProfilesService {
    async getProfile(profileId) {
        AppState.account = null
        const response = await api.get(`api/profiles/${profileId}`)
        logger.log(response.data)
        AppState.account = new Account(response.data)
    }
}

export const profilesService = new ProfilesService();