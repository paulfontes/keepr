import { AppState } from "@/AppState.js";
import { api } from "./AxiosService.js";
import { Account, Profile } from "@/models/Account.js";
import { logger } from "@/utils/Logger.js";
import { Keep } from "@/models/Keep.js";
import { Vault } from "@/models/Vault.js";

class ProfilesService {
    async getProfile(profileId) {
        AppState.profile = null
        const response = await api.get(`api/profiles/${profileId}`)
        // logger.log(response.data)
        AppState.profile = new Profile(response.data)
    }

    async getProfileKeeps(profileId) {
        AppState.keeps = []
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        const keeps = response.data.map((keep) => new Keep(keep))
        // logger.log(keeps)
        AppState.keeps = keeps
    }
    async getProfileVaults(profileId) {
        AppState.vaults = []
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        // logger.log(response.data)
        const vaults = response.data.map((vault) => new Vault(vault))
        AppState.vaults = vaults
    }
}

export const profilesService = new ProfilesService();