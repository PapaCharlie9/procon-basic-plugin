/* BasicPlugin.cs

by PapaCharlie9@gmail.com

Free to use as is in any way you want with no warranty.

*/

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Web;
using System.Data;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Reflection;

using PRoCon.Core;
using PRoCon.Core.Plugin;
using PRoCon.Core.Plugin.Commands;
using PRoCon.Core.Players;
using PRoCon.Core.Players.Items;
using PRoCon.Core.Battlemap;
using PRoCon.Core.Maps;


namespace PRoConEvents
{

//Aliases
using EventType = PRoCon.Core.Events.EventType;
using CapturableEvent = PRoCon.Core.Events.CapturableEvents;

public class BasicPlugin : PRoConPluginAPI, IPRoConPluginInterface
{

/* Inherited:
    this.PunkbusterPlayerInfoList = new Dictionary<String, CPunkbusterInfo>();
    this.FrostbitePlayerInfoList = new Dictionary<String, CPlayerInfo>();
*/

private bool fIsEnabled;
private int fDebugLevel;

public BasicPlugin() {
	fIsEnabled = false;
	fDebugLevel = 2;
}

public enum MessageType { Warning, Error, Exception, Normal };

public String FormatMessage(String msg, MessageType type) {
	String prefix = "[^b" + GetPluginName() + "^n] ";

	if (type.Equals(MessageType.Warning))
		prefix += "^1^bWARNING^0^n: ";
	else if (type.Equals(MessageType.Error))
		prefix += "^1^bERROR^0^n: ";
	else if (type.Equals(MessageType.Exception))
		prefix += "^1^bEXCEPTION^0^n: ";

	return prefix + msg;
}


public void LogWrite(String msg)
{
	this.ExecuteCommand("procon.protected.pluginconsole.write", msg);
}

public void ConsoleWrite(String msg, MessageType type)
{
	LogWrite(FormatMessage(msg, type));
}

public void ConsoleWrite(String msg)
{
	ConsoleWrite(msg, MessageType.Normal);
}

public void ConsoleWarn(String msg)
{
	ConsoleWrite(msg, MessageType.Warning);
}

public void ConsoleError(String msg)
{
	ConsoleWrite(msg, MessageType.Error);
}

public void ConsoleException(String msg)
{
	ConsoleWrite(msg, MessageType.Exception);
}

public void DebugWrite(String msg, int level)
{
	if (fDebugLevel >= level) ConsoleWrite(msg, MessageType.Normal);
}


public void ServerCommand(params String[] args)
{
	List<String> list = new List<String>();
	list.Add("procon.protected.send");
	list.AddRange(args);
	this.ExecuteCommand(list.ToArray());
}


public String GetPluginName() {
	return "BasicPlugin";
}

public String GetPluginVersion() {
	return "0.0.0.1";
}

public String GetPluginAuthor() {
	return "PapaCharlie9";
}

public String GetPluginWebsite() {
	return "TBD";
}

public String GetPluginDescription() {
	return @"
<h1>Your Title Here</h1>
<p>TBD</p>

<h2>Description</h2>
<p>TBD</p>

<h2>Commands</h2>
<p>TBD</p>

<h2>Settings</h2>
<p>TBD</p>

<h2>Development</h2>
<p>TBD</p>
<h3>Changelog</h3>
<blockquote><h4>1.0.0.0 (15-SEP-2012)</h4>
	- initial version<br/>
</blockquote>
";
}




public List<CPluginVariable> GetDisplayPluginVariables() {

	List<CPluginVariable> lstReturn = new List<CPluginVariable>();

	lstReturn.Add(new CPluginVariable("Settings|Debug level", fDebugLevel.GetType(), fDebugLevel));

	return lstReturn;
}

public List<CPluginVariable> GetPluginVariables() {
	return GetDisplayPluginVariables();
}

public void SetPluginVariable(String strVariable, String strValue) {
	if (Regex.Match(strVariable, @"Debug level").Success) {
		int tmp = 2;
		int.TryParse(strValue, out tmp);
		fDebugLevel = tmp;
	}
}


public void OnPluginLoaded(String strHostName, String strPort, String strPRoConVersion) {
	this.RegisterEvents(this.GetType().Name, "OnVersion", "OnServerInfo", "OnResponseError", "OnListPlayers", "OnPlayerJoin", "OnPlayerLeft", "OnPlayerKilled", "OnPlayerSpawned", "OnPlayerTeamChange", "OnGlobalChat", "OnTeamChat", "OnSquadChat", "OnRoundOverPlayers", "OnRoundOver", "OnRoundOverTeamScores", "OnLoadingLevel", "OnLevelStarted", "OnLevelLoaded");
}

public void OnPluginEnable() {
	fIsEnabled = true;
	ConsoleWrite("Enabled!");
}

public void OnPluginDisable() {
	fIsEnabled = false;
	ConsoleWrite("Disabled!");
}


public override void OnVersion(String serverType, String version) { }

public override void OnServerInfo(CServerInfo serverInfo) {
	ConsoleWrite("Debug level = " + fDebugLevel);
}

public override void OnResponseError(List<String> requestWords, String error) { }

public override void OnListPlayers(List<CPlayerInfo> players, CPlayerSubset subset) {
}

public override void OnPlayerJoin(String soldierName) {
}

public override void OnPlayerLeft(CPlayerInfo playerInfo) {
}

public override void OnPlayerKilled(Kill kKillerVictimDetails) { }

public override void OnPlayerSpawned(String soldierName, Inventory spawnedInventory) { }

public override void OnPlayerTeamChange(String soldierName, int teamId, int squadId) { }

public override void OnGlobalChat(String speaker, String message) { }

public override void OnTeamChat(String speaker, String message, int teamId) { }

public override void OnSquadChat(String speaker, String message, int teamId, int squadId) { }

public override void OnRoundOverPlayers(List<CPlayerInfo> players) { }

public override void OnRoundOverTeamScores(List<TeamScore> teamScores) { }

public override void OnRoundOver(int winningTeamId) { }

public override void OnLoadingLevel(String mapFileName, int roundsPlayed, int roundsTotal) { }

public override void OnLevelStarted() { }

public override void OnLevelLoaded(String mapFileName, String Gamemode, int roundsPlayed, int roundsTotal) { } // BF3


} // end BasicPlugin

} // end namespace PRoConEvents



