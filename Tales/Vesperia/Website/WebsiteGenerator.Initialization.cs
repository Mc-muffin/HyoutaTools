﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyoutaTools.Tales.Vesperia.Website {
	public partial class WebsiteGenerator {
		public static Dictionary<string, SCFOMBIN.SCFOMBIN> LoadBattleTextTSS( string dir, Util.GameTextEncoding encoding ) {
			var BattleTextFiles = new Dictionary<string, SCFOMBIN.SCFOMBIN>();

			var files = new System.IO.DirectoryInfo( dir ).GetFiles();
			foreach ( var file in files ) {
				if ( file.Name.StartsWith( "BTL_" ) ) {
					var bin = new ScenarioFile.ScenarioFile( System.IO.Path.Combine( dir, file.Name ), encoding );
					var name = file.Name.Split( '.' )[0];

					var btl = new SCFOMBIN.SCFOMBIN();
					btl.EntryList = bin.EntryList;
					BattleTextFiles.Add( name, btl );
				}
			}

			return BattleTextFiles;
		}

		public static Dictionary<string, SCFOMBIN.SCFOMBIN> LoadBattleTextScfombin( string dir, Util.Endianness endian, string modDir = null ) {
			var BattleTextFiles = new Dictionary<string, SCFOMBIN.SCFOMBIN>();

			var files = new System.IO.DirectoryInfo( dir ).GetFiles();
			foreach ( var file in files ) {
				if ( file.Name.StartsWith( "BTL_" ) ) {
					uint ptrDiff = 0x1888;
					if ( file.Name.StartsWith( "BTL_XTM" ) ) { ptrDiff = 0x1B4C; }

					var bin = new SCFOMBIN.SCFOMBIN( System.IO.Path.Combine( dir, file.Name ), endian, ptrDiff );
					var name = file.Name.Split( '.' )[0];

					if ( modDir != null ) {
						var modBin = new SCFOMBIN.SCFOMBIN( System.IO.Path.Combine( modDir, file.Name ), endian, ptrDiff );
						for ( int i = 0; i < bin.EntryList.Count; ++i ) {
							bin.EntryList[i].EnName = modBin.EntryList[i].JpName;
							bin.EntryList[i].EnText = modBin.EntryList[i].JpText;
						}
					}

					BattleTextFiles.Add( name, bin );
				}
			}

			return BattleTextFiles;
		}

		public static List<uint> GenerateRecordsStringDicList( GameVersion version ) {
			if ( version != GameVersion.X360 && version != GameVersion.PS3 ) {
				throw new Exception( "Unknown game version for records menu: " + version );
			}

			List<uint> records = new List<uint>();

			for ( uint i = 33912371; i < 33912385; ++i ) {
				if ( i == 33912376 ) { continue; }
				records.Add( i );
			}
			records.Add( 33912570u );
			for ( uint i = 33912385; i < 33912392; ++i ) {
				records.Add( i );
			}
			records.Add( 33912571u );
			records.Add( 33912572u );
			records.Add( 33912585u );
			records.Add( 33912586u );
			records.Add( 33912587u );
			records.Add( 33912588u );

			if ( version == GameVersion.PS3 ) {
				// repede snowboarding 1 - 8, team melee, 30 man per character
				for ( uint i = 33912733; i < 33912751; ++i ) {
					records.Add( i );
				}
			} else if ( version == GameVersion.X360 ) {
				records.Add( 33912621u ); // 30 man melee generic
			}

			for ( uint i = 33912392; i < 33912399; ++i ) {
				records.Add( i );
			}
			if ( version.HasPS3Content() ) {
				// usage flynn, patty
				records.Add( 33912399u );
				records.Add( 33912400u );
			}

			return records;
		}

		public static List<ConfigMenuSetting> GenerateSettingsStringDicList( GameVersion version ) {
			if ( version != GameVersion.X360 && version != GameVersion.PS3 ) {
				throw new Exception( "Unknown game version for settings menu: " + version );
			}

			List<ConfigMenuSetting> settings = new List<ConfigMenuSetting>();

			settings.Add( new ConfigMenuSetting( 33912401u, 33912401u + 46u, 33912427u, 33912426u, 33912425u, 33912424u ) ); // msg speed
			settings.Add( new ConfigMenuSetting( 33912402u, 33912402u + 46u, 33912428u, 33912429u, 33912430u, 33912431u ) ); // difficulty
			if ( version == GameVersion.X360 ) {
				settings.Add( new ConfigMenuSetting( 33912403u, 33912403u + 46u, 33912438u, 33912437u ) ); // x360 vibration
			} else {
				settings.Add( new ConfigMenuSetting( 33912679u, 33912681u, 33912438u, 33912437u ) ); // console-neutral vibration
			}
			settings.Add( new ConfigMenuSetting( 33912404u, 33912404u + 46u, 33912432u, 33912433u ) ); // camera controls
			if ( version == GameVersion.PS3 ) {
				settings.Add( new ConfigMenuSetting( 33912751u, 33912752u, 33912443u, 33912444u ) ); // stick/dpad controls
			}
			settings.Add( new ConfigMenuSetting( 33912405u, 33912405u + 46u, 33912439u ) ); // button config
			settings.Add( new ConfigMenuSetting( 33912406u, 33912406u + 46u, 33912436u, 33912435u, 33912434u ) ); // sound
			settings.Add( new ConfigMenuSetting( 33912407u, 33912407u + 46u ) ); // bgm
			settings.Add( new ConfigMenuSetting( 33912408u, 33912408u + 46u ) ); // se
			settings.Add( new ConfigMenuSetting( 33912409u, 33912409u + 46u ) ); // battle se
			settings.Add( new ConfigMenuSetting( 33912413u, 33912413u + 46u ) ); // battle voice
			settings.Add( new ConfigMenuSetting( 33912414u, 33912414u + 46u ) ); // event voice
			settings.Add( new ConfigMenuSetting( 33912422u, 33912422u + 46u ) ); // skit
			settings.Add( new ConfigMenuSetting( 33912423u, 33912423u + 46u ) ); // movie
			if ( version == GameVersion.PS3 ) {
				settings.Add( new ConfigMenuSetting( 33912656u, 33912657u, 33912658u, 33912659u ) ); // item request type
			}
			settings.Add( new ConfigMenuSetting( 33912410u, 33912410u + 46u, 33912438u, 33912437u ) ); // engage cam
			settings.Add( new ConfigMenuSetting( 33912411u, 33912411u + 46u, 33912438u, 33912437u ) ); // dynamic cam
			settings.Add( new ConfigMenuSetting( 33912412u, 33912412u + 46u, 33912438u, 33912437u ) ); // field boundary
			settings.Add( new ConfigMenuSetting( 33912415u, 33912415u + 46u, 33912438u, 33912437u ) ); // location names
			settings.Add( new ConfigMenuSetting( 33912416u, 33912416u + 46u, 33912438u, 33912437u ) ); // skit titles
			settings.Add( new ConfigMenuSetting( 33912417u, 33912417u + 46u, 33912438u, 33912437u ) ); // skit subs
			settings.Add( new ConfigMenuSetting( 33912418u, 33912418u + 46u, 33912438u, 33912437u ) ); // movie subs
			settings.Add( new ConfigMenuSetting( 33912420u, 33912420u + 46u, 33912440u, 33912441u, 33912442u ) ); // font
			if ( version == GameVersion.X360 ) {
				settings.Add( new ConfigMenuSetting( 33912419u, 33912419u + 46u, 33912439u ) ); // brightness
				settings.Add( new ConfigMenuSetting( 33912421u, 33912421u + 46u, 33912439u ) ); // marketplace
			} else {
				settings.Add( new ConfigMenuSetting( 33912713u, 33912714u, 33912439u ) ); // brightness & screen pos
			}
			settings.Add( new ConfigMenuSetting( 33912595u, 33912596u, 33912597u ) ); // reset to default

			return settings;
		}

		public List<List<ScenarioData>> CreateScenarioIndexGroups( ScenarioType type, MapList.MapList maplist, string scenarioDatFolder, string scenarioDatFolderMod = null, Util.GameTextEncoding encoding = Util.GameTextEncoding.ShiftJIS ) {
			SortedDictionary<int, ScenarioWebsiteName> websiteNames = ScenarioWebsiteName.GenerateWebsiteNames( this.Version );
			Util.Assert( maplist.MapNames.Count == websiteNames.Count );

			List<ScenarioData> scenes = new List<ScenarioData>();
			List<ScenarioFile.ScenarioFile> scenarioFiles = new List<ScenarioFile.ScenarioFile>();
			bool haveSeenEP_030_010 = false;
			foreach ( var d in websiteNames ) {
				var names = maplist.MapNames[d.Key];
				if ( d.Value.Type != type ) {
					continue;
				}

				string episodeID = names.Name3 == "dummy" ? names.Name1 : names.Name3;

				// the game has this file twice in scenario.dat, so ignore the first instance we encounter, as presumably the game would overwrite the first instance with the second?
				if ( !haveSeenEP_030_010 && episodeID == "EP_030_010" ) {
					haveSeenEP_030_010 = true;
					continue;
				}

				int num = d.Key;
				try {
					var orig = new ScenarioFile.ScenarioFile( System.IO.Path.Combine( scenarioDatFolder, num + ".d" ), encoding );
					if ( scenarioDatFolderMod != null ) {
						var mod = new ScenarioFile.ScenarioFile( System.IO.Path.Combine( scenarioDatFolderMod, num + ".d" ), encoding );
						Util.Assert( orig.EntryList.Count == mod.EntryList.Count );
						for ( int i = 0; i < orig.EntryList.Count; ++i ) {
							orig.EntryList[i].EnName = mod.EntryList[i].JpName;
							orig.EntryList[i].EnText = mod.EntryList[i].JpText;
						}
					}
					orig.EpisodeID = episodeID;
					scenarioFiles.Add( orig );
					scenes.Add( new ScenarioData() { ScenarioDatIndex = num, EpisodeId = episodeID, HumanReadableName = d.Value.Description != null ? d.Value.Description : episodeID } );
				} catch ( System.IO.FileNotFoundException ) { }
			}

			foreach ( var s in scenarioFiles.OrderBy( x => x.EpisodeID ) ) {
				this.ScenarioFiles.Add( s.EpisodeID, s );
			}
			return ScenarioData.ProcessScenesToGroups( scenes );
		}

		public void ScenarioAddSkits( List<List<ScenarioData>> groups ) {
			List<TO8CHLI.SkitInfo> skitsToProcess = new List<TO8CHLI.SkitInfo>();
			foreach ( var skit in Skits.SkitInfoList ) {
				if ( skit.Category == 0 ) {
					skitsToProcess.Add( skit );
				}
			}
			skitsToProcess.Sort();

			for ( int i = 0; i < groups.Count; ++i ) {
				var group = groups[i];
				for ( int j = 0; j < group.Count; ++j ) {
					var scene = group[j];

					ScenarioData nextScene = null;
					if ( j != group.Count - 1 ) {
						nextScene = group[j + 1];
					} else {
						if ( i != groups.Count - 1 ) {
							nextScene = groups[i + 1][0];
						}
					}

					uint nextScenarioId = 1000000u;
					if ( nextScene != null ) {
						string scenarioIdStr = nextScene.EpisodeId.Substring( 3, 7 ).Replace( "_", "" );
						nextScenarioId = UInt32.Parse( scenarioIdStr.TrimStart( '0' ) );
					}

					List<TO8CHLI.SkitInfo> skitsToRemove = new List<TO8CHLI.SkitInfo>();
					foreach ( var skit in skitsToProcess ) {
						uint skitTrigger = skit.FlagTrigger % 1000000u;
						if ( skitTrigger < nextScenarioId ) {
							skitsToRemove.Add( skit );

							if ( !scene.Skits.Contains( skit ) ) {
								scene.Skits.Add( skit );
							}
						}
					}
					foreach ( var skit in skitsToRemove ) {
						skitsToProcess.Remove( skit );
					}
				}
			}
		}
	}
}
