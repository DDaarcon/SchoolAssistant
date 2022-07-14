export default class ServerConnection {
    constructor(
        private _baseUrl: string) { }

    getResponseAsync(handler?: string, params?: {}): Promise<Response> {
        return fetch(this.prepareUrl(handler, params), {
            method: 'GET'
        });
    }

    async getAsync<TResponse>(handler?: string, params?: {}) {
        let response = await this.getResponseAsync(handler, params);

        return this.parseResponseAsync<TResponse>(response);
    }

    async postAsync<TResponse>(handler?: string, params?: {}, body?: {}) {
        let response = await fetch(this.prepareUrl(handler, params), {
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
                //@ts-ignore
                'RequestVerificationToken': antiforgeryToken
            },
            body: JSON.stringify(body)
        });
        return this.parseResponseAsync<TResponse>(response);
    }

    private prepareUrl(handler?: string, params?: {}) {
        let queryStr = '?'

        if (handler?.length)
            queryStr = `?handler=${handler}&`;

        if (params) {
            let paramNames = Object.keys(params);

            const paramsStr = paramNames.map((paramName) => `${paramName}=${params[paramName]}`).join('&');

            queryStr += paramsStr;
        }

        return `${this._baseUrl}${queryStr}`;
    }

    private async parseResponseAsync<TResponse>(result: Response) {
        try {
            return (await result.json()) as TResponse;
        }
        catch {
            return undefined;
        }
    }
}

export type ResponseJson = {
    success: boolean;
    message: string | null;
}

export type SaveResponseJson = ResponseJson & {
    id?: number;
}