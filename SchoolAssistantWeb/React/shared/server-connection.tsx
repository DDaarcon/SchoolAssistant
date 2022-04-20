export default class ServerConnection {
    constructor(
        private _baseUrl: string)
    {

    }

    async getAsync<TResponse>(handler: string, params?: {}) {
        let response = await fetch(this.prepareUrl(handler, params), {
            method: 'GET'
        });

        let responseJson = (await response.json()) as TResponse;

        return responseJson;
    }

    async postAsync<TResponse>(handler: string, params?: {}, body?: {}) {
        let response = await fetch(this.prepareUrl(handler, params), {
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
                //@ts-ignore
                'RequestVerificationToken': antiforgeryToken
            },
            body: JSON.stringify(body)
        });

        let responseJson = (await response.json()) as TResponse;

        return responseJson;
    }

    private prepareUrl(handler: string, params?: {}) {
        let queryStr = `?handler=${handler}`;

        if (params) {
            let paramNames = Object.keys(params);

            for (let paramName of paramNames) {
                queryStr += `&${paramName}=${params[paramName]}`;
            }
        }

        return `${this._baseUrl}${queryStr}`;
    }
}

export type ResponseJson = {
    success: boolean;
    message: string | null;
}