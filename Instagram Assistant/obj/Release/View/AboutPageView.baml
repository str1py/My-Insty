   M S B A M L   `   `   `  ����   Instagram Assistant,    &Instagram_Assistant.View.AboutPageView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� .+��   � RowDefinitions  �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  � ColumnDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��HELP AND SUPPORT��$��0��$��1��$��1��$��12��$��Center=�
�������   TextBlockH1Style#���   �� $��2��$��3��$��1��$��26�� �� Orientation$ 
Horizontal=�.+��� $	��100��$>��8pack://application:,,,/Resources/Images/insta-splash.png��$��0,0,20,0q��� $	��650��$
��Wrap=�  TextBlockH3Style#���  $i��cThis app is designed to promote and automate your work in Instagram. More description will be soon.���� $��4��$��3��$��Center=�$��1��$��26��$��0,0,0,1q�
��������� $��6��$��3��$��1��$��26��  TextBlockH2Style#���  $,��&Information about programm and license���� $ Vertical=�$��7��$��3��$��1��$��10��.+��� $ 
Horizontal=�.+��� #���  $��Programm state:���� $��5,0,0,0q�
�������#���  $
��Bold"����� *ProgramState+�� $ 
Horizontal=�.+��� #���  $��Product version:���� $��5,0,0,0q�
�������#���  $
��Bold"����� *	Version+�� $ 
Horizontal=�.+��� #���  $"��The validity of the license:���� $��5,0,0,0q�
�������#���  $
��Bold"����� *	License+�� $ Vertical=�$��7��$��3��$��20��$��3��.+���   MainButtonStyle#���  $��25��$��0,0,0,10q�{�.  $��Update license���� #���  $��25��{�. $��Check updates������ *CheckUpdateCommand+�� $��8��$��3��$��Center=�$��1��$��26��$��0,0,0,1q�
��������� $��10��$��3��$��1��$��26��#���  $��Technical support���� $ Vertical=�$��11��$��4��$��1��$��10��.+��� $ 
Horizontal=�.+��� #���  	Telegram:�� $��5,0,0,0q�
�������#���  $
��Bold"�$��@Anulyev���� $ 
Horizontal=�.+��� #���  VK:�� $��5,0,0,0q�
�������#���  $
��Bold"�$��vk.com/pavelanulyev���� $ 
Horizontal=�.+��� #���  
Instagram:�� $��5,0,0,0q�
�������#���  $
��Bold"�$ ��instagram.com/pavelanulyev���� $ 
Horizontal=�.+��� #���  $��E-mail:���� $��5,0,0,0q�
�������#���  $
��Bold"�$��dev.anujevpavel@gmail.com���� $��12��$��3��$��Center=�$��1��$��26��$��0,0,0,1q�
��������� $��14��$��3��$��1��$��26��#���  $��Support project��� y��� *CheckVisibility+  �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��20��$��28��
���   � J� Opacity$	 0.6Q��� $��4��$��7��$��10��$��14��
���   �
�������$��1q�$ 1Q��� $��9��$��10��$��10��$��8��y��� *CheckProgressVisibility+.+���   TextBlockMainStyle#���  $��Center=�$��Check for updates...��$��0,0,0,10q��   ProgressBar#���   � IsIndeterminate .   SecondColorBrush#���  �� -    ��Name$ 
NewVersion��$��
20,15,20,0q�$��4��$��7��$��10��$��14��y��� *NewVersionVisibility+.+��� .+��� $��Center=�$��Center=�#���  $��New version is avalible��$��0,0,0,20q��� $��0,0,0,10q�$ 
Horizontal=�.+��� $ 
Horizontal=�.+�A� -    A�Name$ AppIcon��$��35��$��35������  j� Stretch$ UniformToFill=� �� ImageSource$> 8pack://application:,,,/Resources/Images/insta-splash.png���� $��10,0,0,0q�$ Vertical=�.+��� .+��� #���  $��Center=�$��My Insy Alpha��$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�.+���   Smaller#���  #���  $��	Version: ���� #���  #���  ���� *UpdateVersion+�� $ 
Horizontal=�.+��� #���  #���  $��	Release: ���� #���  #���  ���� *DownloadRelease+�� $��	150,0,0,0q�$ 
Horizontal=�$��Center=�$��Center=�.+��� $��Update��  SettingsButtonStyle#���   	 Regular#��� 	 ���� *UpdateNowCommand+�� .+��� $	��150��.���� #���  $��Disabled=�$
��Auto=�#���  {�.  $
��Wrap=�$��Helvetica Regular)����� *	ChangeLog+�� -   $ UptodateVersion��$��
20,15,20,0q�$��4��$��7��$��10��$��14��y��� *UptodateVersionVisibility+.+��� .+��� $��Center=�$��Center=�#���  $$��My Insty has latest update yet��$��0,0,0,20q��� $��0,0,0,10q�$ 
Horizontal=�.+��� $ 
Horizontal=�.+�A� $��35��$��35������ $ UniformToFill=�$> 8pack://application:,,,/Resources/Images/insta-splash.png���� $��10,0,0,0q�$ Vertical=�.+��� .+��� #���  $��Center=�$��My Insy Alpha��$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�.+��� #���  #���  $��	Version: ���� #���  #���  ���� *UpdateVersion+�� $ 
Horizontal=�.+��� #���  #���  $��
Download: ���� #���  #���  ���� *DownloadRelease+�� $��13��$��2��$��3��$��13��.+��� $��Close��#���  ���� *CancleUpdateCommand+