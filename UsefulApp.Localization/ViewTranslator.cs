using System.Linq;
using System.Reflection;
using Arsis.AIS.UI.Common.Localization.Attributes;
using Arsis.AIS.UI.Common.Localization.Configuration;
using Gtk;

namespace Arsis.AIS.UI.Common.Localization
{
    public static class ViewTranslator
    {
        public static void Translate(object translatableControl, TranslationConfiguration configuration)
        {
            TranslateCustomControl(translatableControl, configuration);

            var members = translatableControl.GetType().GetMembers().Where(p => CustomAttributeExtensions.GetCustomAttribute<TranslatorControlAttribute>((MemberInfo)p) != null);

            foreach (var memberInfo in members)
            {
                var attribute = memberInfo.GetCustomAttribute<TranslatorControlAttribute>();
                var text = configuration.Translations.GetTranslation(attribute.Name);

                var control = GetControl(translatableControl, memberInfo);

                if (control == null) continue;

                if (control is Label label)
                {
                    label.Text = text;
                }
                else
                {
                    var method = control.GetType().GetMethod("SetText");
                    if (method != null)
                    {
                        method.Invoke(control, new object[] { text });
                    }
                }
            }
        }

        private static void TranslateCustomControl(object translatableControl, TranslationConfiguration configuration)
        {
            var customControlMemberInfos = translatableControl.GetType().GetMembers().Where(p => p.GetCustomAttribute<TranslatorCustomControlAttribute>() != null);
            var customControls = customControlMemberInfos.Select<MemberInfo, object>(t => GetControl(translatableControl, t));

            foreach (var cc in customControls)
            {
                Translate(cc, configuration);
            }
        }

        private static object GetControl(object view, MemberInfo memberInfo)
        {
            object control = null;

            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    control = fieldInfo.GetValue(view);
                    break;

                case PropertyInfo propertyInfo:
                    control = propertyInfo.GetValue(view);
                    break;
            }

            return control;
        }
    }
}