﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VKSharp.Core.Enums;
using VKSharp.Data.Request;
using VKSharp.Helpers;
using VKSharp.Helpers.PrimitiveEntities;

namespace VKSharp {
    public partial class VKApi {
        public async Task<StructEntity<bool>> VideosReportAsync(int ownerId, uint photoId, ReportReason? reason = null, string comment="", string search_query="") {
            var req = new VKRequest<StructEntity<bool>> {
                MethodName = "video.report",
                Parameters =
                    new Dictionary<string, string> {
                        { "owner_id", ownerId.ToNCString() },
                        { "photo_id", photoId.ToNCString() },
                        { "reason", reason == null ? "" : ( (int) reason ).ToNCString() },
                        {"comment",comment},
                        {"search_query",search_query}
                    }
            };
            if ( this.IsLogged )
                req.Token = this.CurrenToken;
            return ( await this._executor.ExecAsync( req ) ).Data.FirstOrDefault();
        }
        public async Task<StructEntity<bool>> VideosReportCommentAsync(int ownerId, uint commentId, ReportReason? reason = null) {
            var req = new VKRequest<StructEntity<bool>> {
                MethodName = "video.reportComment",
                Parameters =
                    new Dictionary<string, string> {
                        { "owner_id", ownerId.ToNCString() },
                        { "comment_id", commentId.ToNCString() },
                        { "reason", reason == null ? "" : ( (int) reason ).ToNCString() }
                    }
            };
            if ( this.IsLogged )
                req.Token = this.CurrenToken;
            return ( await this._executor.ExecAsync( req ) ).Data.FirstOrDefault();
        }
        public async Task<StructEntity<bool>> VideosDeleteAlbumAsync(uint albumId, uint? groupId = null) {
            var req = new VKRequest<StructEntity<bool>> {
                MethodName = "video.deleteAlbum",
                Parameters =
                    new Dictionary<string, string> {
                        { "album_id", albumId.ToNCString() },
                        { "group_id", MiscTools.NullableString( groupId ) }
                    }
            };
            if (!this.IsLogged)
                throw new InvalidOperationException("This method requires auth!");
            req.Token = this.CurrenToken;
            return (await this._executor.ExecAsync(req)).Data.FirstOrDefault();
        }
        public async Task<StructEntity<bool>> VideosDeleteAsync(uint videoId, int? ownerId = null) {
            var req = new VKRequest<StructEntity<bool>> {
                MethodName = "video.delete",
                Parameters =
                    new Dictionary<string, string> {
                        { "video_id", videoId.ToNCString() },
                        { "owner_id", MiscTools.NullableString( ownerId ) }
                    }
            };
            if (!this.IsLogged)
                throw new InvalidOperationException("This method requires auth!");
            req.Token = this.CurrenToken;
            return (await this._executor.ExecAsync(req)).Data.FirstOrDefault();
        }
        public async Task<StructEntity<bool>> VideosDeleteCommentAsync(int ownerId, uint commentId) {
            var req = new VKRequest<StructEntity<bool>> {
                MethodName = "video.deleteComment",
                Parameters =
                    new Dictionary<string, string> {
                    { "owner_id", ownerId.ToNCString() },
                    { "comment_id", commentId.ToNCString() },
                }
            };
            if (!this.IsLogged)
                throw new InvalidOperationException("This method requires auth!");
            req.Token = this.CurrenToken;
            return (await this._executor.ExecAsync(req)).Data.FirstOrDefault();
        }
        public async Task<StructEntity<bool>> VideosRestoreCommentAsync(int ownerId, uint commentId) {
            var req = new VKRequest<StructEntity<bool>> {
                MethodName = "video.restoreComment",
                Parameters = new Dictionary<string, string> {
                    { "owner_id", ownerId.ToNCString() },
                    { "comment_id", commentId.ToNCString() },
                }
            };
            if (!this.IsLogged)
                throw new InvalidOperationException("This method requires auth!");
            req.Token = this.CurrenToken;
            return (await this._executor.ExecAsync(req)).Data.FirstOrDefault();
        }
        public async Task<StructEntity<bool>> VideosRemoveTagAsync(uint videoId, uint tagId, int? ownerId = null) {
            var req = new VKRequest<StructEntity<bool>> {
                MethodName = "video.removeTag",
                Parameters =
                    new Dictionary<string, string> {
                        { "video_id", videoId.ToNCString() },
                        { "tag_id", tagId.ToNCString() },
                        { "owner_id", MiscTools.NullableString( ownerId ) }
                    }
            };
            if (!this.IsLogged)
                throw new InvalidOperationException("This method requires auth!");
            req.Token = this.CurrenToken;
            return (await this._executor.ExecAsync(req)).Data.FirstOrDefault();
        }
    }
}