﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fortnox.SDK.Entities;
using Fortnox.SDK.Requests;
using Fortnox.SDK.Utility;

namespace Fortnox.SDK.Connectors.Base
{
    /// <remarks/>
    public abstract class EntityConnector<TEntity> : BaseConnector where TEntity : class
    {
        protected async Task<T> SendAsync<T>(EntityRequest<T> request)
        {
            if (request.Entity != null)
            {

                var requestJson = request.UseEntityWrapper ? 
                    Serializer.Serialize(new EntityWrapper<T>(request.Entity)): 
                    Serializer.Serialize(request.Entity);
                request.Content = Encoding.UTF8.GetBytes(requestJson);
            }

            var responseData = await SendAsync((BaseRequest)request).ConfigureAwait(false);
            var responseJson = Encoding.UTF8.GetString(responseData);

            return request.UseEntityWrapper
                ? Serializer.Deserialize<EntityWrapper<T>>(responseJson).Entity
                : Serializer.Deserialize<T>(responseJson);
        }

        protected async Task<TEntity> BaseCreate(TEntity entity)
        {
            var request = new EntityRequest<TEntity>()
            {
                Resource = Resource,
                Method = HttpMethod.Post,
                Entity = entity
            };

            return await SendAsync(request).ConfigureAwait(false);
        }

        protected async Task<TEntity> BaseUpdate(TEntity entity, params string[] indices)
        {
            var request = new EntityRequest<TEntity>()
            {
                Resource = Resource,
                Indices = indices.ToList(),
                Method = HttpMethod.Put,
                Entity = entity
            };

            return await SendAsync(request).ConfigureAwait(false);
        }

        protected async Task BaseDelete(params string[] indices)
        {
            var request = new BaseRequest()
            {
                Resource = Resource,
                Indices = indices.ToList(),
                Method = HttpMethod.Delete
            };

            await SendAsync(request).ConfigureAwait(false);
        }

        protected async Task<TEntity> BaseGet(params string[] indices)
        {
            var request = new EntityRequest<TEntity>()
            {
                Resource = Resource,
                Indices = indices.ToList(),
                Method = HttpMethod.Get,
            };

            return await SendAsync(request).ConfigureAwait(false);
        }
        
        protected async Task<byte[]> DoDownloadActionAsync(string documentNumber, Action action)
        {
            if (!action.IsDownloadAction())
                throw new Exception("Invalid action type");

            var request = new FileDownloadRequest()
            {
                Resource = Resource,
                Indices = new List<string>{ documentNumber, action.GetStringValue() },
            };

            return await SendAsync(request).ConfigureAwait(false);
        }

        protected async Task<TEntity> DoActionAsync(string documentNumber, Action action)
        {
            if (action.IsDownloadAction())
                throw new Exception("Invalid action type");

            var request = new EntityRequest<TEntity>()
            {
                Resource = Resource,
                Indices = new List<string> {documentNumber, action.GetStringValue()},
                Method = action.GetMethod()
            };

            return await SendAsync(request).ConfigureAwait(false);
        }
    }
}
