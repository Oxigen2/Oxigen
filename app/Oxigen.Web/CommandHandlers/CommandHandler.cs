using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIPresentation.CommandHandlers.Processors;
using System.Web.SessionState;


namespace OxigenIIPresentation.CommandHandlers
{
  /// <summary>
  /// Provides general methods for command HTTP handlers
  /// </summary>
  internal abstract class CommandHandler : IHttpHandler, IRequiresSessionState 
  {
    public virtual bool IsReusable
    {
      get { return false; }
    }

    public virtual void ProcessRequest(HttpContext context)
    {
      context.Response.Clear();

      ChannelCommandConfigurationSection commandSection = (ChannelCommandConfigurationSection)System.Configuration.ConfigurationManager.GetSection("channelCommandSet");

      // is this a GET request
      if (context.Request.QueryString["command"] != null)
      {
        string commandName = context.Request.QueryString["command"];

        if (commandSection.ChannelCommands[commandName] == null)
        {
          context.Response.Write(ErrorWrapper.SendError("Invalid command."));
          return;
        }

        string commandType = commandSection.ChannelCommands[commandName].Type;

        GetCommandProcessor getCommandProcessor = GetCommandProcessor<GetCommandProcessor>(commandType, context);

        if (getCommandProcessor != null)
          context.Response.Write(getCommandProcessor.Execute(context.Request.QueryString));
      }
      // is this a POST request
      else if (context.Request.Form["command"] != null)
      {
        string[] postVars = context.Request.Form["command"].Split(new string[] { ",," }, StringSplitOptions.None);

        if (postVars.Length < 2)
        {
          context.Response.Write(ErrorWrapper.SendError("Not enough parameters."));
          return;
        }
        
        if (commandSection.ChannelCommands[postVars[0]] == null)
        {
          context.Response.Write(ErrorWrapper.SendError("Invalid command."));
          return;
        }

        string commandType = commandSection.ChannelCommands[postVars[0]].Type;

        PostCommandProcessor postCommandProcessor = GetCommandProcessor<PostCommandProcessor>(commandType, context);

        if (postCommandProcessor != null)
          context.Response.Write(postCommandProcessor.Execute(postVars));
      }
      else
        context.Response.Write(ErrorWrapper.SendError("Command parameter not specified."));
    }

    /// <summary>
    /// Gets the appropriate command processor with the specified type
    /// </summary>
    /// <param name="commandType">Type of the processor to instantiate</param>
    /// <param name="context">HttpContext object that requested this instantiation</param>
    /// <returns>a CommandProcessor that will perform the requested command action, null if instantiation failed.</returns>
    protected T GetCommandProcessor<T>(string commandType, HttpContext context) where T : CommandProcessor
    {
      Type type = null;
      T commandProcessor = null;

      try
      {
        type = Type.GetType(commandType);
      }
      catch (Exception ex)
      {
        context.Response.Write(ErrorWrapper.SendError(ex.Message));
        return null;
      }

      try
      {
        commandProcessor = (T)(Activator.CreateInstance(type, new object[]{ context.Session }));
      }
      catch (Exception ex)
      {
        context.Response.Write(ErrorWrapper.SendError(ex.Message));
        return null;
      }

      return commandProcessor;
    }
  }
}
