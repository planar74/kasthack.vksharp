﻿using System;
using System.Collections.Generic;
using System.Linq;
using VKSharp.Core.Entities;
using VKSharp.Core.ResponseEntities;
using VKSharp.Data.Api;
using VKSharp.Data.Request;
using VKSharp.Helpers;
using VKSharp.Helpers.PrimitiveEntities;

namespace VKSharp {
	public class RequestApi {
		private VKToken CurrentToken {get;set;}
		private bool IsLogged {get;set;}
		public VKRequest<StructEntity<bool>> AccountSetNameInMenu(
			 string name 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "account.setNameInMenu",
				Parameters = new Dictionary<string, string> {
					{ "name", name }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> AccountSetOnline(
			 bool voip = true
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "account.setOnline",
				Parameters = new Dictionary<string, string> {
					{ "voip", (voip?1:0).ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> AccountSetOffline(
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "account.setOffline",
				Parameters = new Dictionary<string, string> {
						}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> AccountUnregisterDevice(
			 string token 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "account.unregisterDevice",
				Parameters = new Dictionary<string, string> {
					{ "token", token }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> AccountSetSilenceMode(
			 string token ,
			 int time ,
			 int chatId ,
			 int userId ,
			 int sound 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "account.setSilenceMode",
				Parameters = new Dictionary<string, string> {
					{ "token", token },
			{ "time", time.ToNCString() },
			{ "chat_id", chatId.ToNCString() },
			{ "user_id", userId.ToNCString() },
			{ "sound", sound.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<long>> AccountGetAppPermissions(
			 uint userId 
			){
			var req = new VKRequest<StructEntity<long>>{
				MethodName = "account.getAppPermissions",
				Parameters = new Dictionary<string, string> {
					{ "user_id", userId.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<StructEntity<long>> AccountBanUser(
			 uint userId 
			){
			var req = new VKRequest<StructEntity<long>>{
				MethodName = "account.banUser",
				Parameters = new Dictionary<string, string> {
					{ "user_id", userId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<EntityList<User>> AccountGetBanned(
			 uint offset = 0,
			 uint count = 20
			){
			var req = new VKRequest<EntityList<User>>{
				MethodName = "account.getBanned",
				Parameters = new Dictionary<string, string> {
					{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<long>> AccountSetInfo(
			 uint? intro = null
			){
			var req = new VKRequest<StructEntity<long>>{
				MethodName = "account.setInfo",
				Parameters = new Dictionary<string, string> {
					{ "intro", MiscTools.NullableString(intro) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<User> AccountGetProfileInfo(
			){
			var req = new VKRequest<User>{
				MethodName = "account.getProfileInfo",
				Parameters = new Dictionary<string, string> {
						}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<int>> AudioAddAlbum(
			 string title ,
			 uint? groupId = 0
			){
			var req = new VKRequest<StructEntity<int>>{
				MethodName = "audio.addAlbum",
				Parameters = new Dictionary<string, string> {
					{ "title", title },
			{ "group_id", MiscTools.NullableString(groupId) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<int>> AudioAdd(
			 int ownerId ,
			 uint audioId ,
			 uint? groupId = null
			){
			var req = new VKRequest<StructEntity<int>>{
				MethodName = "audio.add",
				Parameters = new Dictionary<string, string> {
					{ "owner_id", ownerId.ToNCString() },
			{ "audio_id", audioId.ToNCString() },
			{ "group_id", MiscTools.NullableString(groupId) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<int>> AudioDeleteAlbum(
			 uint albumId ,
			 uint? groupId = null
			){
			var req = new VKRequest<StructEntity<int>>{
				MethodName = "audio.deleteAlbum",
				Parameters = new Dictionary<string, string> {
					{ "album_id", albumId.ToNCString() },
			{ "group_id", MiscTools.NullableString(groupId) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<int>> AudioDelete(
			 int ownerId ,
			 uint audioId 
			){
			var req = new VKRequest<StructEntity<int>>{
				MethodName = "audio.delete",
				Parameters = new Dictionary<string, string> {
					{ "owner_id", ownerId.ToNCString() },
			{ "audio_id", audioId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<EntityList<Audio>> AudioGet(
			 int? offset = null,
			 int? count = null,
			 bool needUser = false,
			 int? ownerId = null,
			 int? albumId = null,
			 ulong[] audioIds = null
			){
			var req = new VKRequest<EntityList<Audio>>{
				MethodName = "audio.get",
				Parameters = new Dictionary<string, string> {
					{ "offset", MiscTools.NullableString(offset) },
			{ "count", MiscTools.NullableString(count) },
			{ "need_user", (needUser?1:0).ToNCString() },
			{ "owner_id", MiscTools.NullableString(ownerId) },
			{ "album_id", MiscTools.NullableString(albumId) },
			{ "audio_ids", audioIds.ToNCStringA() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<Audio> AudioGetById(
			 string[] audios ,
			 bool itunes = false
			){
			var req = new VKRequest<Audio>{
				MethodName = "audio.getById",
				Parameters = new Dictionary<string, string> {
					{ "audios", String.Join(",",audios) },
			{ "itunes", (itunes?1:0).ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<StructEntity<int>> AudioGetCount(
			 int? ownerId = null
			){
			var req = new VKRequest<StructEntity<int>>{
				MethodName = "audio.getCount",
				Parameters = new Dictionary<string, string> {
					{ "owner_id", MiscTools.NullableString(ownerId) }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<Lyrics> AudioGetLyrics(
			 int lyricsId 
			){
			var req = new VKRequest<Lyrics>{
				MethodName = "audio.getLyrics",
				Parameters = new Dictionary<string, string> {
					{ "lyrics_id", lyricsId.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<SimpleEntity<string>> AudioGetUploadServer(
			 int albumId ,
			 int? groupId = null
			){
			var req = new VKRequest<SimpleEntity<string>>{
				MethodName = "audio.getUploadServer",
				Parameters = new Dictionary<string, string> {
					{ "album_id", albumId.ToNCString() },
			{ "group_id", MiscTools.NullableString(groupId) }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<StructEntity<bool>> AudioReorder(
			 int audioId ,
			 int? ownerId = null,
			 int? after = null,
			 int? before = null
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "audio.reorder",
				Parameters = new Dictionary<string, string> {
					{ "audio_id", audioId.ToNCString() },
			{ "owner_id", MiscTools.NullableString(ownerId) },
			{ "after", MiscTools.NullableString(after) },
			{ "before", MiscTools.NullableString(before) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<Audio> AudioRestore(
			 int audioId ,
			 int? ownerId = null
			){
			var req = new VKRequest<Audio>{
				MethodName = "audio.restore",
				Parameters = new Dictionary<string, string> {
					{ "audio_id", audioId.ToNCString() },
			{ "owner_id", MiscTools.NullableString(ownerId) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<Audio> AudioSave(
			 string server ,
			 string audio ,
			 string hash ,
			 string artist = "",
			 string title = ""
			){
			var req = new VKRequest<Audio>{
				MethodName = "audio.save",
				Parameters = new Dictionary<string, string> {
					{ "server", server },
			{ "audio", audio },
			{ "hash", hash },
			{ "artist", artist },
			{ "title", title }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> AuthCheckPhone(
			 string phone ,
			 string clientSecret ,
			 int? clientId = null
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "auth.checkPhone",
				Parameters = new Dictionary<string, string> {
					{ "phone", phone },
			{ "client_secret", clientSecret },
			{ "client_id", MiscTools.NullableString(clientId) }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<ConfirmResult> AuthConfirm(
			 uint clientId ,
			 string clientSecret ,
			 string phone ,
			 string code ,
			 string password = "",
			 bool testMode = false,
			 int? intro = null
			){
			var req = new VKRequest<ConfirmResult>{
				MethodName = "auth.confirm",
				Parameters = new Dictionary<string, string> {
					{ "client_id", clientId.ToNCString() },
			{ "client_secret", clientSecret },
			{ "phone", phone },
			{ "code", code },
			{ "password", password },
			{ "test_mode", (testMode?1:0).ToNCString() },
			{ "intro", MiscTools.NullableString(intro) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> BoardCloseTopic(
			 uint groupId ,
			 uint topicId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "board.closeTopic",
				Parameters = new Dictionary<string, string> {
					{ "group_id", groupId.ToNCString() },
			{ "topic_id", topicId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> BoardDeleteComment(
			 uint groupId ,
			 uint topicId ,
			 uint commentId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "board.deleteComment",
				Parameters = new Dictionary<string, string> {
					{ "group_id", groupId.ToNCString() },
			{ "topic_id", topicId.ToNCString() },
			{ "comment_id", commentId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> BoardDeleteTopic(
			 uint groupId ,
			 uint topicId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "board.deleteTopic",
				Parameters = new Dictionary<string, string> {
					{ "group_id", groupId.ToNCString() },
			{ "topic_id", topicId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> BoardFixTopic(
			 uint groupId ,
			 uint topicId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "board.fixTopic",
				Parameters = new Dictionary<string, string> {
					{ "group_id", groupId.ToNCString() },
			{ "topic_id", topicId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> BoardUnfixTopic(
			 uint groupId ,
			 uint topicId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "board.unfixTopic",
				Parameters = new Dictionary<string, string> {
					{ "group_id", groupId.ToNCString() },
			{ "topic_id", topicId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> BoardRestoreComment(
			 uint groupId ,
			 uint topicId ,
			 uint commentId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "board.restoreComment",
				Parameters = new Dictionary<string, string> {
					{ "group_id", groupId.ToNCString() },
			{ "topic_id", topicId.ToNCString() },
			{ "comment_id", commentId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<EntityList<DatabaseEntry>> DatabaseGetCountries(
			 bool needAll = false,
			 string code = "",
			 uint offset = 0,
			 uint count = 100
			){
			var req = new VKRequest<EntityList<DatabaseEntry>>{
				MethodName = "database.getCountries",
				Parameters = new Dictionary<string, string> {
					{ "need_all", (needAll?1:0).ToNCString() },
			{ "code", code },
			{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<EntityList<DatabaseEntry>> DatabaseGetRegions(
			 uint countryId ,
			 string q = "",
			 uint offset = 0,
			 uint count = 100
			){
			var req = new VKRequest<EntityList<DatabaseEntry>>{
				MethodName = "database.getRegions",
				Parameters = new Dictionary<string, string> {
					{ "country_id", countryId.ToNCString() },
			{ "q", q },
			{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<DatabaseEntry> DatabaseGetStreetsById(
			params uint[] streetIds 
			){
			var req = new VKRequest<DatabaseEntry>{
				MethodName = "database.getStreetsById",
				Parameters = new Dictionary<string, string> {
					{ "street_ids", streetIds.ToNCStringA() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<EntityList<DatabaseEntry>> DatabaseGetCountriesById(
			params uint[] countryIds 
			){
			var req = new VKRequest<EntityList<DatabaseEntry>>{
				MethodName = "database.getCountriesById",
				Parameters = new Dictionary<string, string> {
					{ "country_ids", countryIds.ToNCStringA() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<DatabaseCity> DatabaseGetCitiesById(
			params uint[] cityIds 
			){
			var req = new VKRequest<DatabaseCity>{
				MethodName = "database.getCitiesById",
				Parameters = new Dictionary<string, string> {
					{ "city_ids", cityIds.ToNCStringA() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<DatabaseCity> DatabaseGetCities(
			 uint countryId ,
			 uint? regionId = null,
			 string q = "",
			 bool needAll = false,
			 uint offset = 0,
			 uint count = 100
			){
			var req = new VKRequest<DatabaseCity>{
				MethodName = "database.getCities",
				Parameters = new Dictionary<string, string> {
					{ "country_id", countryId.ToNCString() },
			{ "region_id", MiscTools.NullableString(regionId) },
			{ "q", q },
			{ "need_all", (needAll?1:0).ToNCString() },
			{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<DatabaseEntry> DatabaseGetUniversities(
			 uint? countryId = null,
			 uint? cityId = null,
			 string q = "",
			 uint offset = 0,
			 uint count = 100
			){
			var req = new VKRequest<DatabaseEntry>{
				MethodName = "database.getUniversities",
				Parameters = new Dictionary<string, string> {
					{ "country_id", MiscTools.NullableString(countryId) },
			{ "city_id", MiscTools.NullableString(cityId) },
			{ "q", q },
			{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<DatabaseEntry> DatabaseGetSchools(
			 uint? cityId = null,
			 string q = "",
			 uint offset = 0,
			 uint count = 100
			){
			var req = new VKRequest<DatabaseEntry>{
				MethodName = "database.getSchools",
				Parameters = new Dictionary<string, string> {
					{ "city_id", MiscTools.NullableString(cityId) },
			{ "q", q },
			{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<EntityList<DatabaseEntry>> DatabaseGetFaculties(
			 uint universityId ,
			 uint offset = 0,
			 uint count = 100
			){
			var req = new VKRequest<EntityList<DatabaseEntry>>{
				MethodName = "database.getFaculties",
				Parameters = new Dictionary<string, string> {
					{ "university_id", universityId.ToNCString() },
			{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<EntityList<DatabaseEntry>> DatabaseGetChairs(
			 uint facultyId ,
			 uint offset = 0,
			 uint count = 100
			){
			var req = new VKRequest<EntityList<DatabaseEntry>>{
				MethodName = "database.getChairs",
				Parameters = new Dictionary<string, string> {
					{ "faculty_id", facultyId.ToNCString() },
			{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<StructEntity<int>> DocsAdd(
			 uint docId ,
			 int ownerId ,
			 string accessKey = ""
			){
			var req = new VKRequest<StructEntity<int>>{
				MethodName = "docs.add",
				Parameters = new Dictionary<string, string> {
					{ "doc_id", docId.ToNCString() },
			{ "owner_id", ownerId.ToNCString() },
			{ "access_key", accessKey }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> DocsDelete(
			 uint docId ,
			 int ownerId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "docs.delete",
				Parameters = new Dictionary<string, string> {
					{ "doc_id", docId.ToNCString() },
			{ "owner_id", ownerId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<SimpleEntity<string>> DocsGetUploadServer(
			 uint? groupId = null
			){
			var req = new VKRequest<SimpleEntity<string>>{
				MethodName = "docs.getUploadServer",
				Parameters = new Dictionary<string, string> {
					{ "group_id", MiscTools.NullableString(groupId) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<SimpleEntity<string>> DocsGetWallUploadServer(
			 uint? groupId = null
			){
			var req = new VKRequest<SimpleEntity<string>>{
				MethodName = "docs.getWallUploadServer",
				Parameters = new Dictionary<string, string> {
					{ "group_id", MiscTools.NullableString(groupId) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<EntityList<Document>> DocsGet(
			 int? ownerId = null,
			 uint offset = 0,
			 uint count = 100
			){
			var req = new VKRequest<EntityList<Document>>{
				MethodName = "docs.get",
				Parameters = new Dictionary<string, string> {
					{ "owner_id", MiscTools.NullableString(ownerId) },
			{ "offset", offset.ToNCString() },
			{ "count", count.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<Document> DocsGetById(
			params Tuple<int,int>[] docs 
			){
			var req = new VKRequest<Document>{
				MethodName = "docs.getById",
				Parameters = new Dictionary<string, string> {
					{ "docs", String.Join(",",docs.Select(a=>a.Item1 +"_" +a.Item2)) }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<Document> DocsSave(
			 string file ,
			 string title ,
			params string[] tags 
			){
			var req = new VKRequest<Document>{
				MethodName = "docs.save",
				Parameters = new Dictionary<string, string> {
					{ "file", file },
			{ "title", title },
			{ "tags", String.Join(",",tags) }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> FriendsAdd(
			 uint userId ,
			 string text = ""
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "friends.add",
				Parameters = new Dictionary<string, string> {
					{ "user_id", userId.ToNCString() },
			{ "text", text }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> FriendsDeleteAllRequests(
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "friends.deleteAllRequests",
				Parameters = new Dictionary<string, string> {
						}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> FriendsDelete(
			 uint userId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "friends.delete",
				Parameters = new Dictionary<string, string> {
					{ "user_id", userId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<bool>> FriendsDeleteList(
			 uint listId 
			){
			var req = new VKRequest<StructEntity<bool>>{
				MethodName = "friends.deleteList",
				Parameters = new Dictionary<string, string> {
					{ "list_id", listId.ToNCString() }
				}
			};
				req.Token = CurrentToken;
			
			return req;
		}
		public VKRequest<StructEntity<int>> FriendsGetAppUsers(
			){
			var req = new VKRequest<StructEntity<int>>{
				MethodName = "friends.getAppUsers",
				Parameters = new Dictionary<string, string> {
						}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
		public VKRequest<StructEntity<int>> FriendsGetMutual(
			 uint listId 
			){
			var req = new VKRequest<StructEntity<int>>{
				MethodName = "friends.getMutual",
				Parameters = new Dictionary<string, string> {
					{ "list_id", listId.ToNCString() }
				}
			};
			if (IsLogged){
				req.Token = CurrentToken;
			}
			return req;
		}
	}
}