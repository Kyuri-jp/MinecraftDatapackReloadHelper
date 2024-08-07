﻿using MinecraftDatapackReloadHelper.API.Rcon;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class ConnectionTest
    {
        internal static async Task ConnectingTesterAsync()
        {
            var connection = RconConnector.GetRconInst();
            try
            {
                Console.WriteLine(await connection.SendCommandAsync("say [MDRH] Rcon was connected"));
            }
            catch (Exception ex)
            {
                Display.Console.Error(ex.Message);
                Display.Console.Error(ex.StackTrace);
            }
        }
    }
}