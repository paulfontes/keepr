import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { Account, Profile } from "@/models/Account.js";
import { logger } from "@/utils/Logger.js";

class ProfilesService {
    async getProfile(profileId) {
        AppState.profile = null
        const response = await api.get(`api/profiles/${profileId}`)
        logger.log(response.data)
        AppState.profile = new Profile(response.data)
    }
}

export const profilesService = new ProfilesService();